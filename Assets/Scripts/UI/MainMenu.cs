using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;

    private bool m_setttingsOn;

    private void Awake()
    {
        optionsMenu.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        m_setttingsOn = !m_setttingsOn;
        optionsMenu.SetActive(m_setttingsOn);
        mainMenu.SetActive(!m_setttingsOn);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
