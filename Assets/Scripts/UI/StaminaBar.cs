using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StaminaBar : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private Image fill;
        private float m_halfHeight;
        
        
        private void Update()
        {
            SetStaminaValue(player.stamina);
            m_halfHeight = ((RectTransform)fill.transform.parent).rect.height / 2f;
        }

        private void SetStaminaValue(float playerStamina)
        {
            fill.transform.localPosition =
                Vector3.Lerp(Vector3.down * m_halfHeight, Vector3.up * m_halfHeight, playerStamina);
        }
    }
}
