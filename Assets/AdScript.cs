using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;
using GoogleMobileAds.Api;
using TMPro;
using System;


[RequireComponent (typeof (Button))]
public class AdScript : MonoBehaviour, IUnityAdsListener {





    #if UNITY_IOS
    private string gameId = "4071832";
    #elif UNITY_ANDROID
    private string gameId = "4071833";
    #endif

    #if DEVELOPMENT_BUILD
    bool testMode = true;
    #else
    bool testMode = false;
    #endif

    Button myButton;
    TMP_Text text;


    public string mySurfacingId = "Rewarded_Android";
    public string surfacingId = "Banner_Android";




    void Start () {
   
        myButton = GetComponent <Button> ();

        // Set interactivity to be dependent on the Ad Unit or legacy Placement’s status:
        myButton.interactable = Advertisement.IsReady (mySurfacingId); 

        // Map the ShowRewardedVideo function to the button’s click listener:
//if (myButton) myButton.onClick.AddListener (ShowRewardedVideo);
        StartCoroutine(ShowBannerWhenInitialized());

        // Initialize the Ads listener and service:
        Advertisement.AddListener (this);
        Advertisement.Initialize (gameId, false);
        MobileAds.Initialize(initStatus => { });
    }

        IEnumerator ShowBannerWhenInitialized () {
        while (!Advertisement.isInitialized) {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition (BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show (surfacingId);
    }

    

    // Implement a function for showing a rewarded video ad:
    public void ShowRewardedVideo () {
         if (Advertisement.IsReady()){
        Advertisement.Show (mySurfacingId);
        Debug.Log("Ad should show");
         }
         else{
             Debug.Log("Reward not ready");
         }
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady (string surfacingId) {
        // If the ready Ad Unit or legacy Placement is rewarded, activate the button: 
        if (surfacingId == mySurfacingId) {        
            myButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish (string surfacingId, ShowResult showResult) {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished) {
            //scoreScript = GetComponent<DoublePoints>();
            doubleScore();
            // Reward the user for watching the ad to completion.
        } else if (showResult == ShowResult.Skipped) {
            // Do not reward the user for skipping the ad.
        } else if (showResult == ShowResult.Failed) {
            Debug.LogWarning ("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidError (string message) {
        // Log the error.
    }

    public void OnUnityAdsDidStart (string surfacingId) {
        // Optional actions to take when the end-users triggers an ad.
    } 

        public void ShowInterstitialAd() {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady()) {
            Debug.Log("Add should show interstitial");
            Advertisement.Show();
        } 
        else {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }
    public void doubleScore(){
        Debug.Log("Double");
        int score = 0;
        score = PlayerPrefs.GetInt ("Score", score);
        score = score * 2;
        Debug.Log(score);
        PlayerPrefs.SetInt ("Score", score);
    }
    
}