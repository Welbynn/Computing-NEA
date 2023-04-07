using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public GameObject Panel1;
    public GameObject Panel2;
    public GameObject Panel3;
    public CompleteText completeText;
    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;

    public void Update()
    {
        // set's complete text value to the complete text value at the corresponding index
        Text1.SetActive(completeText.levelTexts[0]);
        Text2.SetActive(completeText.levelTexts[1]);
        Text3.SetActive(completeText.levelTexts[2]);
    }

    // Loads the first level when level 1 button is clicked
    public void Level1()
    {
        SceneManager.LoadScene(2);
        unloadScene();
    }

    // Loads level 2 only if level 1 has been completed
    public void Level2()
    {
        if (completeText.levelTexts[0])
        {
            SceneManager.LoadScene(3);
            unloadScene();
        }
    }
    
    // Loads level 3 only if level 2 has been completed
    public void Level3()
    {
        if (completeText.levelTexts[1])
        {
            SceneManager.LoadScene(4);
            unloadScene();
        }
    }

    // Loads the main menu
    public void back()
    {
        SceneManager.LoadScene(0);
        unloadScene();
    }

    // Sets other panels to be inactive so level select always starts on level 1
    void unloadScene()
    {
        Panel1.SetActive(true);
        Panel2.SetActive(false);
        Panel3.SetActive(false);

    }
}
