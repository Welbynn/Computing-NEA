using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Loads the level selection scene when the play button is clicked 
    public void onClickPlay()
    {
        SceneManager.LoadScene(1);
    }

    // Exits the game when the exit button is pressed 
    public void ExitGame()
    {
        Application.Quit();
    }
}
