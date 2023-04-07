using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool paused = false;
    public static bool settings = false;
    public GameObject PauseMenuUI;
    public GameObject SettingsMenu;
    public PlayerMovement playerMovement;

    // Update is called once per frame
    void Update()
    {
        // if the key to pause the game is pressed then one of two things will happen
        if (Input.GetKeyDown(KeyCode.Escape) && !settings)
        {
            // if the game is already paused and the settings menu isn't open unpause the game and hide the pause menu
            if (paused)
            {
                Resume();
            }
            // otherwise pause the game and set the pause menu as active
            else
            {
                playerMovement.enabled = false;
                PauseMenuUI.SetActive(true);
                Time.timeScale = 0f;
                paused = true;
            }
        }
    }

    // Written as a separate function for access in the onClick() section
    public void Resume()
    {
        playerMovement.enabled = true;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    // onCLick() for options button
    public void Menu()
    {
        SettingsMenu.SetActive(true);
        PauseMenuUI.SetActive(false);
        Time.timeScale = 0f;
        paused = true;
        settings = true;
    }

    // closes the settings menu
    public void ExitMenu()
    {
        SettingsMenu.SetActive(false);
        PauseMenuUI.SetActive(true);
        settings = false;
    }

    // onClick() for exit button
    public void Exit()
    {
        SceneManager.LoadScene(0);
        Resume();
    }
}
