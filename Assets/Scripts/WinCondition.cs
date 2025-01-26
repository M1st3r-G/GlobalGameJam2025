using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private TextMeshProUGUI timerText;

    private void Update()
    {
        timerText.text = $"{timeManager.timeTaken[0]} : {timeManager.timeTaken[1]} : {timeManager.timeTaken[2]}";
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(()=>progressBar.progress >= 1f);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(10f);
        SceneManager.LoadScene(0);
    }
}
