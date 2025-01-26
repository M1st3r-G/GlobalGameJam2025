using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private TextMeshProUGUI HighScore;
    [SerializeField] private TextMeshProUGUI LastTime;
    private bool m_setttingsOn;
    
    private int m_highScoreHour;
    private int m_highScoreMinute;
    private int m_highScoreSecond;
    private void Awake()
    {
        LastTime.text = $"Last Time: {PlayerPrefs.GetInt("h", 0)} : {PlayerPrefs.GetInt("m", 0)} : {PlayerPrefs.GetInt("s", 0)}";
        if (PlayerPrefs.GetInt("h", 0) < PlayerPrefs.GetInt("hh", 99) &&
            PlayerPrefs.GetInt("m", 0) < PlayerPrefs.GetInt("hm", 99) &&
            PlayerPrefs.GetInt("s", 0) < PlayerPrefs.GetInt("hs", 99))
        {
            m_highScoreHour = PlayerPrefs.GetInt("h", 0);
            m_highScoreMinute = PlayerPrefs.GetInt("m", 0);
            m_highScoreSecond = PlayerPrefs.GetInt("s", 0);
            
            
            PlayerPrefs.SetInt("hh", m_highScoreHour);
            PlayerPrefs.SetInt("hm", m_highScoreMinute);
            PlayerPrefs.SetInt("hs", m_highScoreSecond);
        }
        HighScore.text = $"High Score: {m_highScoreHour} : {m_highScoreMinute} : {m_highScoreSecond}";
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        print("Quit!");
        Application.Quit();
    }
}
