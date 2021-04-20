using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreScript : MonoBehaviour
{
    public PlayGamesScript playGamesScript;
    public TMP_Text text;
    int score = 0;
    void Start()
    {
    score = PlayerPrefs.GetInt ("Score", score);
    text.text = "Score"+ ": "+ score.ToString(); 
    int leaderInt = 0;
    leaderInt = PlayerPrefs.GetInt("Score",0);
    long leaderScore = Convert.ToInt64(leaderInt);
    playGamesScript.PostToLeaderBoard(leaderScore);
    Debug.Log(leaderScore);
    }
}

