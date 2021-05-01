using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine;
using System;

public class PlayGamesScript : MonoBehaviour
{
    string LeaderboardId = "CggI3Yua5wIQAhAC";
	long scorecall = 0;
    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();       
		PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
  
        PlayGamesPlatform.Instance.Authenticate(loginCallback, true);
    }

    public void PostToLeaderBoard(long score)
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
		{
            Social.ReportScore(score, "CggI3Yua5wIQAhAC", (bool success) => {
                Debug.Log("Report Success");
            });
        }
		else{
			scorecall = score;
			PlayGamesPlatform.Instance.Authenticate(reportCallback,false);
		}
	}
 
	public void reportCallback(Boolean success){
		if(success){
			Social.ReportScore(scorecall, "CggI3Yua5wIQAhAC", (bool success) => {
                Debug.Log("Report Success");
            });
		}
	}

    public void ShowLeaderBoard()
	{
		if (PlayGamesPlatform.Instance.localUser.authenticated)
		{
			PlayGamesPlatform.Instance.ShowLeaderboardUI();
		}
		else
		{
			PlayGamesPlatform.Instance.Authenticate(ShowLeaderBoardCall,false);
		}
	}

	public void ShowLeaderBoardCall(Boolean success){
		if(success){
    		Social.ShowLeaderboardUI();
		}
	}

		public void ShowAchievementCall(Boolean success){
		if(success){
				Social.ShowAchievementsUI();
		}
	}

       	public void LoginToPlayGames()
	{
			PlayGamesPlatform.Instance.Authenticate(loginCallback,false);
	}

    public void loginCallback(Boolean success){
        if (success)
		{
			Debug.Log("signed in");
		}
		else
		{
			Debug.Log("Sign in error");
		}
    }
	public void FirstBlock(){
		Social.ReportProgress("CggI3Yua5wIQAhAI", 100.0f, (bool success) => {
			if(success){
				Debug.Log("sucess");
			}
		});
	}

		public void SmashHundredBlock(){
		Social.ReportProgress("CggI3Yua5wIQAhAJ", 100.0f, (bool success) => {
			if(success){
				Debug.Log("sucess");
			}		
		});
	}

		public void SmashFiftyBlock(){
		Social.ReportProgress("CggI3Yua5wIQAhAK", 100.0f, (bool success) => {
			if(success){
				Debug.Log("sucess");
			}		
		});
	}

	
	public void ShowAcheivments(){
		if (PlayGamesPlatform.Instance.localUser.authenticated)
		{
				Social.ShowAchievementsUI();
		}
		else
		{
			PlayGamesPlatform.Instance.Authenticate(ShowAchievementCall,false);
		}
	}

}

