using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [SerializeField] private Transform m_startPoint;
    [SerializeField] private Transform m_endPoint;

    private Slider m_slider;

    private float m_totalHeight = 0;
    private float m_characterHeight = 0;
    private float m_progress = 0;

    private void Start()
    {
        m_slider = GetComponent<Slider>();
    }

    private void Update()
    {
        m_totalHeight = m_endPoint.position.y - m_startPoint.position.y;
        m_characterHeight = m_target.position.y - m_startPoint.position.y;
        m_progress = Mathf.Clamp01(m_characterHeight / m_totalHeight);


        m_slider.value = m_progress;
    }
}
