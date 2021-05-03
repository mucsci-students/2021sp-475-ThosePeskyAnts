using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject AntTarget;

    public float maxLevelTime = 100; 
    public float timeLeft = 0; 
    public bool isGameOver = false; 

    public GameObject player;
    public Text timerText;
    public Sugar sugar;

    public GameObject gameWonScreen; 
    public GameObject gameLostScreen; 
    
    void Start()
    {
        timeLeft = maxLevelTime; 
        // player = GameObject.Find("Player");
        timerText = GameObject.Find("TimeLeftIndicator").GetComponent<Text>();
        // gameWonScreen = GameObject.Find("GameWonScreen");
        // gameLostScreen = GameObject.Find("GameLostScreen");
    }

    void Update()
    {
        // determine if game is over 
        // game is over when an ant reaches the sugar 
        if (sugar.hasAntReachedSugar)
        {
            isGameOver = true;
            // pause game
            Time.timeScale = 0; 
            GameLost();
        }

        if (!isGameOver)
        {
            // determine if game won 
            if (timeLeft <= 0)
            {
                isGameOver = true; 
                GameWon();
                return ; 
            }
            // calc new time
            timeLeft -= Time.deltaTime; 
            // display time 
            timerText.text = Mathf.FloorToInt(timeLeft).ToString();
        }
    }

    void GameWon()
    {
        Debug.Log("Congrats! You stopped the ants!");
        gameWonScreen.SetActive(true);
    }

    void GameLost()
    {
        gameLostScreen.SetActive(true);
    }
}
