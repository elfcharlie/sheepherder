using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ShowHighScore : MonoBehaviour
{
    public GameObject textBox;
    private TextMeshPro textBoxText;
    public int numberOfLevels;
    private int[] highScores = new int[4];
    private string highScoreText = "";
    private string tutorialText = "Run with Milo into one of the levels. Make sure to get all sheep into the sheepfolds to unlock more levels.";
    
    void Start()
    {
        textBoxText = textBox.GetComponent<TextMeshPro>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        SetHighScoreText();
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        textBoxText.SetText(tutorialText);
        highScoreText = "";
    }

    void GetHighScores()
    {
        for (int i = 1; i <= numberOfLevels; i++)
        {
            highScores[i-1] = PlayerPrefs.GetInt("level" + i.ToString(), 0);           
        }
    }

    void SetHighScoreText()
    {
        GetHighScores();
        int i = 1;
        foreach (int score in highScores)
        {
            highScoreText += "Level " + i.ToString() + ": ";
            for (int j = 1; j <= score; j++)
            {
                highScoreText += "<sprite=0>";
            }
            i++;
            highScoreText += "<br>";
        }
        textBoxText.SetText(highScoreText);
    }
}
