using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Input")][SerializeField] private InputActionReference movementAction;
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private InputActionReference invinPowerUpAction;
    [SerializeField] private InputActionReference speedPowerUpAction;
    
    [Header("Movement")] [SerializeField] private float defaultFloatSpeed = 1f;
    [SerializeField] private float floatieness = 0.1f;
    
    [Header("Health")][SerializeField] private float maxHealth = 100f;
    [SerializeField] private float shrinkSpeed = 1f;
    [SerializeField] private Transform bubble;
    
    [Header("Projectile")] [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10;
    
    [Header("UI")][SerializeField] private TextMeshProUGUI invinText;
    [SerializeField] private TextMeshProUGUI speedText;
    public float stamina => m_health / maxHealth;
    
    private Rigidbody2D m_rigidbody;
    private float m_health;
    private Vector2 m_lastDirection;

    private bool m_isInvincible;
    public void AddInvincibility() => invcibilityAmount++;
    private int invcibilityAmount
    {
        get => m_invcibilityAmount;
        set
        {
            m_invcibilityAmount = value;
            invinText.text = value.ToString();
        }
    }
    private int m_invcibilityAmount;

    public void AddSuperSpeed() => superSpeedAmount++;
    private int superSpeedAmount
    {
        get => m_superSpeedAmount;
        set
        {
            m_superSpeedAmount = value;
            speedText.text = value.ToString();
        }
    }
    private int m_superSpeedAmount;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_health = maxHealth;
        
        movementAction.action.Enable();
        attackAction.action.Enable();
        attackAction.action.performed += Attack;
        invinPowerUpAction.action.Enable();
        invinPowerUpAction.action.performed += OnTriggerInvincibilityInput;
        speedPowerUpAction.action.Enable();
        speedPowerUpAction.action.performed += OnTriggerSpeedInput;
    }

    private void OnTriggerInvincibilityInput(InputAction.CallbackContext ctx)
    {
        if (invcibilityAmount <= 0) return;
        
        invcibilityAmount--;
        TriggerInvincibility();
    }

    private void OnTriggerSpeedInput(InputAction.CallbackContext ctx)
    {
        if(superSpeedAmount <= 0)return;
        
        superSpeedAmount--;
        TriggerSuperSpeed();
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        LooseHealth(10);
        GameObject tmp = Instantiate(projectilePrefab, transform.position + (Vector3)m_lastDirection*0.5f, Quaternion.identity);
        tmp.GetComponent<Rigidbody2D>().velocity = m_lastDirection.normalized * projectileSpeed;
    }

    public void AddPushForce(Vector2 force) => m_lastDirection += force;

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

    public void TriggerHealing() => m_health += 25f;

    private void TriggerSuperSpeed()
    {
        defaultFloatSpeed *= 2f;
        StartCoroutine(BetterInvoke(3f, () => defaultFloatSpeed /= 2f));
    }

    private void TriggerInvincibility()
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
