using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float maxLevelTime = 100; 
    public float timeLeft = 0; 
    public bool hasGameStarted = false;
    public bool isGameOver = false; 
    public bool isSandbox = false; 

    public GameObject player;
    public Text timerText;
    public Sugar sugar;

    public GameObject gameWonScreen; 
    public GameObject gameLostScreen; 
    public GameObject pauseMenu;
    public GameObject startScreen; 

    public Text fpsCounter; 
    public float totalFPS = 0.0f;
    public int framesPassed = 0;

    void Start()
    {
        timeLeft = maxLevelTime; 
        // player = GameObject.Find("Player");
        timerText = GameObject.Find("TimeLeftIndicator").GetComponent<Text>();

        // display time 
        timerText.text = Mathf.FloorToInt (timeLeft).ToString();

        isGameOver = false; 

        StartScreen ();

    }

    void Update ()
    {

        // update fps counter 
        float fps = 1 / Time.unscaledDeltaTime;
        totalFPS += fps;
        framesPassed += 1;
        if (framesPassed >= 10) 
        {
            fpsCounter.text = Mathf.RoundToInt(totalFPS / framesPassed).ToString();
            framesPassed = 0;
            totalFPS = 0;
        }

        if (hasGameStarted && !isGameOver)
        {

            // check for user to pause the game 
            if (Input.GetKeyDown (KeyCode.Escape))
            {
                if (IsPaused()) ResumeGame ();
                else PauseGame ();
            }

            if (!isSandbox)
            {
                // determine if game is over 
                // game is over when an ant reaches the sugar 
                if (sugar.hasAntReachedSugar)
                {
                    GameLost ();
                }

                // determine if game won 
                if (timeLeft <= 0)
                {
                    GameWon ();
                    return ; 
                }
                // calc new time
                timeLeft -= Time.deltaTime; 
                // display time 
                timerText.text = Mathf.FloorToInt (timeLeft).ToString();
            }
        }
    }

    void GameWon()
    {
        Debug.Log("Congrats! You stopped the ants!");
        isGameOver = true;
        // Pause game objects 
        Time.timeScale = 0f; 
        // re-enable mouse so player can interact with menu 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
        gameWonScreen.SetActive (true);
        SoundManager.PlaySound ("Victory");
    }

    void GameLost()
    {
        isGameOver = true;
        // pause game objects 
        Time.timeScale = 0f; 
        // re-enable mouse so player can interact with menu 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
        gameLostScreen.SetActive(true);
        SoundManager.PlaySound ("Defeat");
    }

    public void StartScreen ()
    {
        Time.timeScale = 0f;
        hasGameStarted = false; 
        // re-enable mouse so player can interact with menu 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
        startScreen.SetActive(true);
    }

    public void StartGame ()
    {
        Time.timeScale = 1f;
        hasGameStarted = true; 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
        startScreen.SetActive(false);
    }

    public void PauseGame ()
    {
        // Setting the timeScale to 0 
        // will stop the movement of anything 
        // that uses the timeScale, effectively
        // pausing the game 
        Time.timeScale = 0f; 
        // re-enable mouse so player can interact with menu 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
        // show pause menu 
        pauseMenu.SetActive (true);
    }

    public void ResumeGame ()
    {
        // Resetting the timeScale to 1 
        // will unpause the game 
        Time.timeScale = 1;
        // lock and hide mouse 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
        // hide pause menu 
        pauseMenu.SetActive (false);
    }

    public bool IsPaused ()
    {
        return Time.timeScale == 0f; 
    }
}
