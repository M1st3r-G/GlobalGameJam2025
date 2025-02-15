using System.Collections;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EdgeCollider2D))]
    public class LightningController : MonoBehaviour
    {
        [SerializeField] private GameObject lightningShort;
        [SerializeField] private GameObject lightningLong;
        
        private LightningStorm m_parent;
        private EdgeCollider2D m_collider;
        private Transform m_startTransform;
        private Transform m_endTransform;

        private GameObject m_visual;
        
        private void Awake()
        {
            m_collider = GetComponent<EdgeCollider2D>();
            m_collider.isTrigger = true;
            lightningLong.SetActive(false);
            lightningShort.SetActive(false);
        }

        public void SetUp(Transform cloud, Transform cloud1, LightningStorm thunder)
        {
            m_parent = thunder;
            m_collider.points = new[] { (Vector2)cloud.position, (Vector2)cloud1.position };
            
            StartCoroutine(AttackLoop());
            m_startTransform = cloud;
            m_endTransform = cloud1;
            
            Vector3 cloudDelta = cloud1.position - cloud.position;
            m_visual = cloudDelta.magnitude > 5.05f ? lightningLong : lightningShort;
            
            //m_visual.transform.localScale = new Vector3(cloudDelta.magnitude, 1f, 1f);
            m_visual.transform.localPosition = cloud.position + cloudDelta / 2f;
            float calcAngle = Mathf.Atan2(cloudDelta.y, cloudDelta.x)*Mathf.Rad2Deg + 45f;
            m_visual.transform.rotation = Quaternion.Euler(0f, 0f, calcAngle);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;

            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            Debug.Assert(player is not null, "Player is not found");
            player.LooseHealth(m_parent.damage);
        }

        private void CustomSetActive(bool active)
        {
            m_collider.enabled = active;
            m_visual?.SetActive(active);
        }

        private IEnumerator AttackLoop()
        {
            while (true)
            {
                print("Hide");
                CustomSetActive(false);
                yield return new WaitForSeconds(m_parent.thunderDeath);
                print("Show");
                CustomSetActive(true);
                yield return new WaitForSeconds(m_parent.thunderAlive);
            }
        }
    }
}