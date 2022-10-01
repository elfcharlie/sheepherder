using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManagerLevelSelect : MonoBehaviour
{
    public GameObject inGameMenuUI;
    public GameObject pauseMenuUI;

    void Start()
    {
        inGameMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        inGameMenuUI.SetActive(false);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        inGameMenuUI.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
