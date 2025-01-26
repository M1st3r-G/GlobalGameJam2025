using System.Collections;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private PlayerController player;
    
    
    private void Update()
    {
        timerText.text = $"{timeManager.timeTaken[0]} : {timeManager.timeTaken[1]} : {timeManager.timeTaken[2]}";
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(()=>progressBar.progress >= 1f);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(10f);
        PlayerPrefs.SetInt("h", timeManager.timeTaken[0]);
        PlayerPrefs.SetInt("m", timeManager.timeTaken[1]);
        PlayerPrefs.SetInt("s", timeManager.timeTaken[2]);
        player.DisableInput();
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
