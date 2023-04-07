using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public GameObject FinishMenu;

    // Gets the currently active scene's build index and loads the next scene (next level)
    public void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
        FinishMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }
    // Returns to main menu
    public void Exit()
    {
        SceneManager.LoadScene(0);
        FinishMenu.SetActive(false);

    }
}
