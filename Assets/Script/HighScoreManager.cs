using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour
{
    bool timeRunning = false;
    public int score1;
    public int score2;
    public int score3;
    float highScoreTimer = 0;
    private string currentLevel = "level";

    void Start()
    {
        currentLevel  +=
            SceneManager.GetActiveScene().buildIndex.ToString();
    }

    void FixedUpdate()
    {
        if(timeRunning)
        {
            highScoreTimer += Time.fixedDeltaTime;
        }
    }

    public void StartTimer()
    {
        timeRunning = true;
    }
    public void StopTimer()
    {
        timeRunning = false;
    }
    public int GetHighScore()
    {
        if(highScoreTimer > score1)
        {
            return 0;
        }
        else if(highScoreTimer > score2)
        {
            return 1;
        }
        else if(highScoreTimer > score3)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }
    public int GetHighScoreInt()
    {
        return (int)highScoreTimer;
    }
    public int GetCurrentHighScore()
    {
        int currentScore = PlayerPrefs.GetInt(currentLevel, 0);
        return currentScore;
    }
    public void SetScore(int score){
        
        if(GetCurrentHighScore() < score)
        {
            PlayerPrefs.SetInt(currentLevel, score);
        }
    }
}
