using System.Collections;
using UnityEngine;

public class SoapSpawner : MonoBehaviour
{
    [SerializeField, ReadOnly] private bool m_spawn = true;
    [Space]
    [SerializeField] private GameObject m_soapPrefab;
    [SerializeField] private Transform m_spawnPoint;
    [SerializeField, Range(0.1f, 60f)] private float m_timer;

    private Coroutine m_BubbleRoutine = null;

    private void OnBecameVisible()
    {
        m_spawn = true;
    }

    private void OnBecameInvisible()
    {
        m_spawn = false;
    }

    private void Start()
    {
        m_BubbleRoutine = StartCoroutine(SpawnBubble());
    }

    private void OnDestroy()
    {
        if (m_BubbleRoutine != null)
        {
            StopCoroutine(m_BubbleRoutine);
        }
    }

    private IEnumerator SpawnBubble()
    {
        while (true)
        {
            if (!m_spawn)
            {
                yield return null;
                continue;
            }
            
            // play sound
            // play animation
            GameObject newBubblePrefab = Instantiate(m_soapPrefab, m_spawnPoint.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(m_timer);
        }
    }
}