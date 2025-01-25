using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float defaultFloatSpeed = 1f;
    [SerializeField] private InputActionReference movementAction;
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private float floatieness = 0.5f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float shrinkSpeed = 1f;
    [SerializeField] private Transform bubble;
    
    private Rigidbody2D m_rigidbody;
    private float m_health;

    private float movementModifier
    {
        get => m_movementModifier;
        set => m_movementModifier = Mathf.Clamp(value, 0f, 1.5f);
    }
    private float m_movementModifier;
    
    private Vector2 m_movementDirection;
    private bool m_isInvincible;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_health = maxHealth;
        
        movementAction.action.Enable();
        attackAction.action.Enable();
        attackAction.action.performed += Attack;
    }

    private void Attack(InputAction.CallbackContext obj) => Debug.Log($"Attack direction: {m_movementDirection}");

    private void Update()
    {
        if (!m_isInvincible)
        {
            m_health -= Time.deltaTime * shrinkSpeed;
            bubble.localScale = Vector3.one * Mathf.Lerp(.8f, 4f, m_health/maxHealth);
            if (m_health <= 0) Death();
        }
        
        Vector2 mInput = movementAction.action.ReadValue<Vector2>();

        if (mInput == Vector2.zero)
        {
            movementModifier -= Time.deltaTime / floatieness;
        }
        else
        {
            movementModifier += Time.deltaTime / floatieness;
            m_movementDirection = mInput.normalized;
        }

        m_rigidbody.velocity = m_movementDirection * (defaultFloatSpeed * movementModifier);
    }

    public void SetInvincible(bool invincible) => m_isInvincible = invincible;

    public void LooseHealth(float amount)
    {
        Debug.Assert(amount >= 0, "Loose health parameter Negative");
        m_health -= amount;
    }
    
    private void Death()
    {
        Debug.Log("Death");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Hit: " + other.gameObject.name);
    }
}
