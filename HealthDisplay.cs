using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public Sprite nullHeart;
    public Sprite fullHeart;

    public Image[] heartArray;

    public PlayerHealth playerHealth;

    // Update is called once per frame
    void Update()
    {
        // sets health to health value from player health script each frame
        health = playerHealth.health;

        for (int i = 0; i < heartArray.Length; i++)
        {
            // if the current heart position is less than current health set to full heart
            if (i < health)
            {
                heartArray[i].sprite = fullHeart;
            }
            // if not set to empty heart
            else
            {
                heartArray[i].sprite = nullHeart;
            }
            // if current heart position is less than maximum health enable the sprite so it appears
            if (i < maxHealth)
            {
                heartArray[i].enabled = true;
            }
            // if not disable the sprite so it is hidden
            else
            {
                heartArray[i].enabled = false;
            }
        }
    }
}
