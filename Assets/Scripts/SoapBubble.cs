using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapBubble : MonoBehaviour
{
    [SerializeField, Tag] private string m_playerTag = "Player";
    [SerializeField] private float m_lifeTime = 60f;
    [SerializeField, ReadOnly] bool m_move = true;

    [SerializeField] private float m_verticalAmplitude = 0.5f;
    [SerializeField] private float m_verticalSpeed = 1f;
    [SerializeField] private float m_horizontalAmplitude = 0.2f;
    [SerializeField] private float m_horizontalSpeed = 0.5f;
    [Space]
    [SerializeField] private float m_growTime = 1f;
    [SerializeField] private AnimationCurve m_growthCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    private Vector3 m_startPosition;
    private float m_currentGrowTime = 0f;
    private Vector3 m_startScale;

    [SerializeField, ReadOnly] private float m_currentLifeTime;

    private IEnumerator Start()
    {
        m_startPosition = transform.position;
        m_startScale = transform.localScale;
        m_currentLifeTime = m_lifeTime;
        transform.localScale = Vector3.zero;

        while (m_currentGrowTime < m_growTime)
        {
            float t = m_currentGrowTime / m_growTime;

            // Add a curve so the growing happens exponentially
            float curveValue = m_growthCurve.Evaluate(t);
            transform.localScale = Vector3.Lerp(Vector3.zero, m_startScale, curveValue);

            m_currentGrowTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = m_startScale;
    }


    private void OnBecameVisible()
    {
        m_move = true;
    }

    private void OnBecameInvisible()
    {
        m_move = false;
    }
    private void Update()
    {
        if (m_move == false)
        {
            return;
        }

        float newY = m_startPosition.y + Mathf.Sin(Time.time * m_verticalSpeed) * m_verticalAmplitude;
        float newX = m_startPosition.x + Mathf.Sin(Time.time * m_horizontalSpeed) * m_horizontalAmplitude;
        transform.position = new Vector3(newX, newY, m_startPosition.z);
        m_currentLifeTime -= Time.deltaTime;

        if (m_currentLifeTime < 0)
        {
            DestroyBubble();
        }
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag(m_playerTag) == false)
        {
            return;
        }

        DestroyBubble();

        // add stamina to player
    }

    public void DestroyBubble()
    {
        // play sound
        // play animation

        Destroy(gameObject);
    }
}
