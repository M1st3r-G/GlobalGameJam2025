using UnityEngine;

public class BorderCollider : MonoBehaviour
{
    [SerializeField] private float forceMultiplier = 3;
    [SerializeField] private float emergencyMultiplier = 5;
    private BoxCollider2D m_leftTrigger;
    private BoxCollider2D m_rightTrigger;
    private BoxCollider2D m_bottomTrigger;
    
    private BoxCollider2D m_leftBox;
    private BoxCollider2D m_rightBox;
    private BoxCollider2D m_bottomBox;
    
    private void Awake()
    {
        m_leftTrigger = gameObject.AddComponent<BoxCollider2D>();
        m_leftTrigger.isTrigger = true;
        m_leftTrigger.size = new Vector2(1.5f, 10f);
        m_leftTrigger.offset = new Vector2(-5f, 0f);

        m_rightTrigger = gameObject.AddComponent<BoxCollider2D>();
        m_rightTrigger.isTrigger = true;
        m_rightTrigger.size = new Vector2(1.5f, 10f);
        m_rightTrigger.offset = new Vector2(5f, 0f);

        m_bottomTrigger = gameObject.AddComponent<BoxCollider2D>();
        m_bottomTrigger.isTrigger = true;
        m_bottomTrigger.size = new Vector2(11.5f, 1.5f);
        m_bottomTrigger.offset = new Vector2(0f, -4.25f);
        
        m_leftBox = gameObject.AddComponent<BoxCollider2D>();
        m_leftBox.size = new Vector2(1.5f, 15f);
        m_leftBox.offset = new Vector2(-6.5f, 0f);;
        
        m_rightBox = gameObject.AddComponent<BoxCollider2D>();
        m_rightBox.size = new Vector2(1.5f, 15f);
        m_rightBox.offset = new Vector2(6.5f, 0f);
        
        m_bottomBox = gameObject.AddComponent<BoxCollider2D>();
        m_bottomBox.size = new Vector2(15f, 1.5f);
        m_bottomBox.offset = new Vector2(0f, -5.75f);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        Vector2 forceDirection = Vector2.down;

        if (other.IsTouching(m_leftTrigger))
            forceDirection = Vector2.right;
        else if (other.IsTouching(m_rightTrigger))
            forceDirection = Vector2.left;
        else if (other.IsTouching(m_bottomTrigger))
            forceDirection = Vector2.up;
        else Debug.LogError("Chat GPT Liegt falsch");

        other.GetComponent<PlayerController>().AddPushForce(forceDirection * forceMultiplier * Time.deltaTime);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        Vector2 forceDirection = Vector2.down;
        if (other.collider.IsTouching(m_leftTrigger))
            forceDirection = Vector2.right;
        else if (other.collider.IsTouching(m_rightTrigger))
            forceDirection = Vector2.left;
        else if (other.collider.IsTouching(m_bottomTrigger))
            forceDirection = Vector2.up;
        else Debug.LogError("Chat GPT Liegt falsch");
        
        other.gameObject.GetComponent<PlayerController>().AddPushForce(forceDirection * forceMultiplier * emergencyMultiplier * Time.deltaTime);
    }
}