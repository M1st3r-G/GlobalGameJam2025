using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class PowerUp: MonoBehaviour
{
    private enum Type
    {
        Shield,
        SuperSpeed,
        Heal,
    }
    
    [SerializeField] private Type type;
    [SerializeField] private Sprite shieldSprite;
    [SerializeField] private Sprite superSpeedSprite;
    [SerializeField] private Sprite healSprite;
    
    private void OnValidate()
    {
        var sr = GetComponent<SpriteRenderer>();
        if (sr is null)
        {
            Debug.LogError("There is no sprite renderer");
            return;
        }

        sr.sprite = type switch
        {
            Type.Shield => shieldSprite,
            Type.SuperSpeed => superSpeedSprite,
            Type.Heal => healSprite,
        };
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        switch (type)
        {
            case Type.Heal:
                player.TriggerHealing();
                break;
            case Type.SuperSpeed:
                player.AddSuperSpeed();
                break;
            case Type.Shield:
            default:
                player.AddInvincibility();
                break;
        }
        
        Destroy(gameObject);
    }
}