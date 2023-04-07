using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    public Slider musicSlider;
    public AudioSource audioSource;
    public SliderValues SliderFloats;

    // if there is no volume set volume to 0.75, if there is then load the volume
    public void Awake()
    {
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 0.75f);
            SliderFloats.volumeValue = 0.75f;
            Load();
        }
        else
        {
            Load();
        }
    }

    // Updates the audio source volume to match the slider volume and saves it
    public void UpdateVolume()
    {
        audioSource.volume = musicSlider.value;

        if (SliderFloats.volumeMute == 1)
        {
            audioSource.mute = true;
        }
        else
        {
            audioSource.mute = false;   
        }

        if (SliderFloats.volumeValue == 0f)
        {
            audioSource.mute = true;
            SliderFloats.volumeMute = 1;
        }
        else
        {
            audioSource.mute = false;
            SliderFloats.volumeMute = 0;
        }
        Save();
    }

    // Saves the current volume so it doesn't change when the player leaves the level
    private void Save()
    {
        SliderFloats.volumeValue = musicSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", SliderFloats.volumeValue);
        PlayerPrefs.SetInt("musicMute", SliderFloats.volumeMute);
    }

    // Loads the previously saved volume
    private void Load()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }
}
