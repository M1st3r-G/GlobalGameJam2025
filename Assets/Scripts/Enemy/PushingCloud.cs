using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
    public class PushingCloud : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private float pushStrength = 5;
        [SerializeField] private bool flipped;
        private Collider2D m_collider;
        private SpriteRenderer m_renderer;
        
        private void Awake()
        {
            ParticleSystem.MainModule main = particles.main;
            main.startSpeed = pushStrength;
            
            m_collider = GetComponent<Collider2D>();
            m_collider.isTrigger = true;
        }

        private void OnValidate()
        {
            transform.localScale = flipped ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;

            Vector2 forceDirection = flipped ? Vector2.left : Vector2.right;
            float locationModifier = other.transform.position.x - transform.position.x;
            
            other.GetComponent<PlayerController>().AddPushForce(forceDirection * pushStrength / locationModifier * Time.deltaTime);
        }
    }
}