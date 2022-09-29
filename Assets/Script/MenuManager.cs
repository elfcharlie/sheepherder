using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject inGameMenuUI;
    public GameObject pauseMenuUI;
    public GameObject finishMenuUI;
    public GameObject timerText;
    private TextMeshProUGUI timerTextUI;
    private HighScoreManager highScoreManager;
    private StarScore starScore;

    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            highScoreManager = GameObject.FindWithTag("HighScoreManager").GetComponent<HighScoreManager>();
        }
        inGameMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        finishMenuUI.SetActive(false);
        Time.timeScale = 1f;
        timerTextUI = timerText.GetComponent<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            timerTextUI.SetText(highScoreManager.GetHighScoreInt().ToString());
        }
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
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void FinishGame(int score)
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        inGameMenuUI.SetActive(false);
        finishMenuUI.SetActive(true);
        highScoreManager.SetScore(score);
        SetFinishStarScore(score);
    }
    private void SetFinishStarScore(int score){
        StartCoroutine(GameObject.FindWithTag("StarScore").GetComponent<StarScore>().SetScore(score));
    }
}
