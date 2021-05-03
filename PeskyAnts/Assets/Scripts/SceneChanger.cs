using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public string level1 = "Level01";
    public int level1Index = 5; 
    public string howToPlayScene = "HowToPlayScene";
    public string creditsScene = "CreditsScene";
    public string mainMenuScene = "MainMenu";
    public string levelSelectScene = "LevelSelect";
    public string sandboxLevelScene = "SandboxLevel";

    public void StartGame()
    {
        SceneManager.LoadScene(level1);
    }

    public void PlayLevelFromIndex (int offset)
    {
        SceneManager.LoadScene (level1Index - 1 + offset);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene(howToPlayScene);
    }

    public void Credits()
    {
        SceneManager.LoadScene(creditsScene);
    }

    public void LevelSelect ()
    {
        SceneManager.LoadScene(levelSelectScene);
    }

    public void SandboxLevel ()
    {
        SceneManager.LoadScene(sandboxLevelScene);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game!");
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
