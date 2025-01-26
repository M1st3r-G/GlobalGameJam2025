using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class DashEnemy : MonoBehaviour
    {
        [SerializeField] private float damage = 10;
        [SerializeField] private float idleTime = 3;
        [SerializeField] private float flightTime = 3;
        [SerializeField] private AnimationCurve yOffset = AnimationCurve.Linear(0, 0, 0, 0);
        [SerializeField] private Transform targetStart;
        [SerializeField] private Transform targetEnd;
        [SerializeField] private float startOffset;

        private SpriteRenderer m_renderer;

        private bool flipped
        {
            get => m_renderer.flipX;
            set => m_renderer.flipX = value;
        }
        
        private void Awake()
        {
            m_renderer = GetComponentInChildren<SpriteRenderer>();
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(startOffset);
            while (true)
            {
                yield return new WaitForSeconds(idleTime);
                float elapsed = 0; // ToDo test
                while (elapsed / flightTime < 1)
                {
                    elapsed += Time.deltaTime;
                    m_renderer.transform.localPosition = Vector3.Lerp(targetStart.localPosition, targetEnd.localPosition, elapsed / flightTime) +
                                         Vector3.up * yOffset.Evaluate(elapsed / flightTime);
                    yield return null;
                }
                flipped = !flipped;
                (targetStart, targetEnd) = (targetEnd, targetStart);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
                other.GetComponent<PlayerController>().LooseHealth(damage);
            else if (other.gameObject.CompareTag("Projectile")) 
                Destroy(transform.parent.gameObject);
        }
    }
}