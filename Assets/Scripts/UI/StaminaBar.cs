using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Slider))]
    public class StaminaBar : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        private Slider m_staminaBar;

        private void Awake() => m_staminaBar = GetComponent<Slider>();

        private void Update() => m_staminaBar.value = player.stamina;
    }
}
