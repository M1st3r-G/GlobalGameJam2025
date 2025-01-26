using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float m_startTime;
    public static TimeManager Instance { get; private set; }

    public int[] timeTaken
    {
        get
        {
            float deltaSeconds = m_startTime - Time.time;
            int hours = (int)(deltaSeconds / 3600f);
            deltaSeconds -= hours * 3600;
            int minutes = (int)(deltaSeconds / 60f);
            deltaSeconds -= minutes * 60;
            int secs = (int)deltaSeconds;
                    
            return new[] {hours, minutes, secs};   
        }
    }

    private void Awake()
    {
        if (Instance is not null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        m_startTime = Time.time;
        DontDestroyOnLoad(gameObject);
    }
}