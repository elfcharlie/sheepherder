using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    private GameObject[] sheep;
    private int sheepAmount;
    private bool isLevelFinished = false;
    private GoalManager goalManagerScript;
    private MenuManager menuManagerScript;
    private DogController dogController;
    private OldManController oldManController;
    private HighScoreManager highScoreManager;
    
    //private StarScore starScore;
    
    void Start()
    {
        isLevelFinished = false;
        sheepAmount = GameObject.FindWithTag("SheepSpawner").GetComponent<SheepSpawner>().sheepAmount;
        goalManagerScript = GameObject.FindWithTag("Finish").GetComponent<GoalManager>();
        menuManagerScript = GameObject.FindWithTag("MenuManager").GetComponent<MenuManager>();
        dogController = GameObject.FindWithTag("Player").GetComponent<DogController>();
        oldManController = GameObject.FindWithTag("OldMan").GetComponent<OldManController>();
        highScoreManager = GameObject.FindWithTag("HighScoreManager").GetComponent<HighScoreManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (sheepAmount == goalManagerScript.GetSheepInGoal())
        {
            
            Finish();
        }
    }
    void Finish()
    {
        dogController.StopMovement();
        oldManController.Finish();
        if (isLevelFinished){
            int score = highScoreManager.GetHighScore();
            menuManagerScript.FinishGame(score);
        }
    }
    public void SetLevelFinished()
    {
        isLevelFinished = true;
    }
}
