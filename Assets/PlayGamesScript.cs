using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine;
using System;

public class PlayGamesScript : MonoBehaviour
{
    string LeaderboardId = "CgkIvO-0284NEAIQAQ";
	long scorecall = 0;
    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();       
		PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
  
        PlayGamesPlatform.Instance.Authenticate(loginCallback, true);
    }

    public void PostToLeaderBoard(long score)
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
		{
            Social.ReportScore(score, "CgkIvO-0284NEAIQAQ", (bool success) => {
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
			Social.ReportScore(scorecall, "CgkIvO-0284NEAIQAQ", (bool success) => {
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
			PlayGamesPlatform.Instance.ShowLeaderboardUI();
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

}

