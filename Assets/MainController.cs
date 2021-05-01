using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainController : MonoBehaviour
{
    public AdScript adScript;
    public adMobAds adMobScript;
    public PlayGamesScript playGamesScript;
    public Button upButton;
    public Button downButton;
    public int score = 0;
    public Button leftButton;
    public Button rightButton;
    Rigidbody rigidBody;
    public GameObject wall_brick;

    // Start is called before the first frame update
    void Start()
    {
        playGamesScript.LoginToPlayGames();
       // upButton = GameObject.FindGameObjectWithTag("UpButton").GetComponent<Button>();
        upButton.onClick.AddListener(() => OnUpButtonClick());
      //  downButton = GameObject.FindGameObjectWithTag("DownButton").GetComponent<Button>();
        downButton.onClick.AddListener(() => OnDownButtonClick());
      //  leftButton = GameObject.FindGameObjectWithTag("LeftButton").GetComponent<Button>();
        leftButton.onClick.AddListener(() => OnLeftButtonClick());
      //  rightButton = GameObject.FindGameObjectWithTag("RightButton").GetComponent<Button>();
        rightButton.onClick.AddListener(() => OnRightButtonClick());
        rigidBody = GetComponent<Rigidbody>(); 
        score = 0;
        PlayerPrefs.SetInt("Score",score);
        adScript.ShowInterstitialAd();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnUpButtonClick(){
        rigidBody.AddForce(250 * Vector3.forward);
    }
       public void OnDownButtonClick(){
        rigidBody.AddForce(-250 * Vector3.forward);
    }
        public void OnLeftButtonClick(){
        rigidBody.AddForce(250 * Vector3.left);
    }
       public void OnRightButtonClick(){
        rigidBody.AddForce(250 * Vector3.right);
    }

    private void OnCollisionEnter(Collision collision){
        WallBrick wall = collision.gameObject.GetComponent<WallBrick>();
        if(wall!=null){
            wall.Destroy();
            score = PlayerPrefs.GetInt("Score",0);
            score = score+1;
            PlayerPrefs.SetInt("Score", score);
            if(score == 1){
                playGamesScript.FirstBlock();
            }
            if(score == 150){
                playGamesScript.SmashFiftyBlock();
            }
            if(score == 100){
                playGamesScript.SmashHundredBlock();
            }
        }
        DeathPlane die = collision.gameObject.GetComponent<DeathPlane>();
        if(die != null){
            adMobScript.GameOver();
            die.gameOver();
        }
    }
}
