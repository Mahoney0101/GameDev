using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class adMobAds : MonoBehaviour
{
    private BannerView bannerView;
    private RewardedAd rewardedAd;

    public void Start()
    {
        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();
        this.RequestInterstitial();
        this.CreateAndLoadRewardedAd();
    }

    private void RequestBanner()
    {
        //string prodBannerId = "ca-app-pub-3423995868360872/9855424045"
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3423995868360872/1121843896";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }
private InterstitialAd interstitial;

private void RequestInterstitial()
{
    //String prodUnitID = "ca-app-pub-3423995868360872/8138855995"
    #if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3423995868360872/8138855995";
    #else
        string adUnitId = "unexpected_platform";
    #endif

    // Initialize an InterstitialAd.
    this.interstitial = new InterstitialAd(adUnitId);
    // Create an empty ad request.
    AdRequest request = new AdRequest.Builder().Build();
    // Load the interstitial with the request.
    this.interstitial.LoadAd(request);
}

public void GameOver()
{
  if (this.interstitial.IsLoaded()) {
    this.interstitial.Show();
  }
}

 public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        this.CreateAndLoadRewardedAd();
    }

public void CreateAndLoadRewardedAd()
    {
            string rewardedAdUnitId;
            //string prodAdUnitId = "ca-app-pub-3423995868360872/7699872291"
        #if UNITY_ANDROID
            rewardedAdUnitId = "ca-app-pub-3423995868360872/7699872291";
        #elif UNITY_IPHONE
            rewardedAdUnitId = "ca-app-pub-3940256099942544/1712485313";
        #else
            rewardedAdUnitId = "unexpected_platform";
        #endif

        this.rewardedAd = new RewardedAd(rewardedAdUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);

    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        // string type = args.Type;
        // double amount = args.Amount;
        // MonoBehaviour.print(
        //     "HandleRewardedAdRewarded event received for "
        //                 + amount.ToString() + " " + type);
        quadScore();
    }

    public void UserChoseToWatchAd()
    {
        if (this.rewardedAd.IsLoaded()) {
        this.rewardedAd.Show();
        }
    }

    public void quadScore(){
        Debug.Log("Double");
        int score = 0;
        score = PlayerPrefs.GetInt ("Score", score);
        score = score * 4;
        Debug.Log(score);
        PlayerPrefs.SetInt ("Score", score);
    }

}