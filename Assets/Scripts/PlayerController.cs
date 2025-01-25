using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Input")][SerializeField] private InputActionReference movementAction;
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private InputActionReference invinPowerUpAction;
    [SerializeField] private InputActionReference speedPowerUpAction;
    
    [Header("Movement")] [SerializeField] private float defaultFloatSpeed = 1f;
    [SerializeField] private float floatieness = 0.1f;
    [SerializeField] private Transform duckTransform;
    [SerializeField] private float rotationSpeedMultiplier;

    [Header("Health")][SerializeField] private float maxHealth = 100f;
    [SerializeField] private float shrinkSpeed = 1f;
    [SerializeField] private Transform bubble;
    [SerializeField] private SpriteRenderer frontRenderer;
    [SerializeField] private SpriteRenderer backRenderer;
    
    [Header("Projectile")] [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10;
    
    [Header("UI")][SerializeField] private TextMeshProUGUI invinText;
    [SerializeField] private TextMeshProUGUI speedText;

    [SerializeField] private VisualBubble normal;
    [SerializeField] private VisualBubble speedBuffed;
    [SerializeField] private VisualBubble shieldBuffed;
    
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

    private bool m_isInPowerUp;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_health = maxHealth;
        
        invcibilityAmount = superSpeedAmount = 0;

        SetDefaultBubble();
        
        movementAction.action.Enable();
        attackAction.action.Enable();
        attackAction.action.performed += Attack;
        invinPowerUpAction.action.Enable();
        invinPowerUpAction.action.performed += OnTriggerInvincibilityInput;
        speedPowerUpAction.action.Enable();
        speedPowerUpAction.action.performed += OnTriggerSpeedInput;
    }

    private void SetDefaultBubble()
    {
        frontRenderer.sprite = normal.front;
        backRenderer.sprite = normal.back;
    }

    private void OnTriggerInvincibilityInput(InputAction.CallbackContext ctx)
    {
        if (invcibilityAmount <= 0 || m_isInPowerUp) return;
        
        invcibilityAmount--;
        TriggerInvincibility();
    }

    private void OnTriggerSpeedInput(InputAction.CallbackContext ctx)
    {
        if(superSpeedAmount <= 0 || m_isInPowerUp) return;
        
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

        duckTransform.Rotate(Vector3.forward,
            (Mathf.Atan2(m_lastDirection.y, m_lastDirection.x) * Mathf.Rad2Deg - 90) * Time.deltaTime * rotationSpeedMultiplier);
    }

    private void FixedUpdate()
    {
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
        m_isInPowerUp = true;
        frontRenderer.sprite = speedBuffed.front;
        backRenderer.sprite = speedBuffed.back;
        StartCoroutine(BetterInvoke(3f, () =>
        {
            SetDefaultBubble();            
            defaultFloatSpeed /= 2f;
            m_isInPowerUp = false;
        }));
    }

    private void TriggerInvincibility()
    {
        SetInvincible(true);
        m_isInPowerUp = true;
        frontRenderer.sprite = shieldBuffed.front;
        backRenderer.sprite = shieldBuffed.back;
        StartCoroutine(BetterInvoke(5f, () =>
        {
            SetDefaultBubble();
            SetInvincible(false);
            m_isInPowerUp = false;
        }));
    }

    private static IEnumerator BetterInvoke(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }
}

[Serializable]
public struct VisualBubble
{
    public Sprite front;
    public Sprite back;
}
