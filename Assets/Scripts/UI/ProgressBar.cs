using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Slider))]
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Transform m_target;
        [SerializeField] private Transform m_startPoint;
        [SerializeField] private Transform m_endPoint;

        private Slider m_slider;

        public float progress { get; private set; }
        public float height { get; private set; }

        private float m_totalHeight;

        private void Start()
        {
            m_slider = GetComponent<Slider>();
        }

        private void Update()
        {
            m_totalHeight = m_endPoint.position.y - m_startPoint.position.y;
            height = m_target.position.y - m_startPoint.position.y;
            progress = Mathf.Clamp01(height / m_totalHeight);


            m_slider.value = progress;
        }
    }
}
