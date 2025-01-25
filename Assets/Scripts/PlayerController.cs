using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Input")][SerializeField] private InputActionReference movementAction;
    [SerializeField] private InputActionReference attackAction;
    [Header("Movement")] [SerializeField] private float defaultFloatSpeed = 1f;
    [SerializeField] private float floatieness = 0.1f;
    [Header("Health")][SerializeField] private float maxHealth = 100f;
    [SerializeField] private float shrinkSpeed = 1f;
    [SerializeField] private Transform bubble;
    [Header("Projectile")] [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10;

    public float stamina => m_health / maxHealth;
    
    private Rigidbody2D m_rigidbody;
    private float m_health;

    private Vector2 m_lastDirection;
    private bool m_isInvincible;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_health = maxHealth;
        
        movementAction.action.Enable();
        attackAction.action.Enable();
        attackAction.action.performed += Attack;
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        LooseHealth(10);
        GameObject tmp = Instantiate(projectilePrefab, transform.position + (Vector3)m_lastDirection*0.5f, Quaternion.identity);
        tmp.GetComponent<Rigidbody2D>().velocity = m_lastDirection.normalized * projectileSpeed;
    }

    private void Update()
    {
        if (!m_isInvincible)
        {
            m_health -= Time.deltaTime * shrinkSpeed;
            bubble.localScale = Vector3.one * Mathf.Lerp(1f, 2.5f, m_health/maxHealth);
            if (m_health <= 0) Death();
        }
        
        Vector2 mInput = movementAction.action.ReadValue<Vector2>();
        if (mInput == Vector2.zero) 
            m_lastDirection *= 0.999f;
        else
        {
            m_lastDirection += mInput.normalized * (floatieness * Time.deltaTime);
            if (m_lastDirection.magnitude > defaultFloatSpeed)
                m_lastDirection = m_lastDirection.normalized * defaultFloatSpeed;
        }

        m_rigidbody.velocity = m_lastDirection;
    }

    private void SetInvincible(bool invincible) => m_isInvincible = invincible;

    public void LooseHealth(float amount)
    {
        Debug.Assert(amount >= 0, "Loose health parameter Negative");
        m_health -= amount;
    }
    
    private void Death()
    {
        SetInvincible(true);
        Debug.Log("Death");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Hit: " + other.gameObject.name);
    }

    public void TriggerHealing() => m_health += 25f;

    public void TriggerSuperSpeed()
    {
        defaultFloatSpeed *= 2f;
        StartCoroutine(BetterInvoke(3f, () => defaultFloatSpeed /= 2f));
    }
    
    public void TriggerInvincibility()
    {
        SetInvincible(true);
        StartCoroutine(BetterInvoke(5f, () => SetInvincible(false)));
    }

    private static IEnumerator BetterInvoke(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }
}
