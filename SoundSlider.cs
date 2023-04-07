using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public Slider soundSlider;
    public SliderValues SliderFloats;

    // if there is no volume set volume to 0.75, if there is then load the volume
    public void Awake()
    {
        if (!PlayerPrefs.HasKey("SoundVolume"))
        {
            PlayerPrefs.SetFloat("SoundVolume", 0.75f);
            SliderFloats.soundValue = 0.75f;
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
        if (SliderFloats.soundValue == 0f)
        {
            SliderFloats.volumeMute = 1;
        }
        else
        {
            SliderFloats.volumeMute = 0;
        }
        Save();
    }

    // Saves the current volume so it doesn't change when the player leaves the level
    private void Save()
    {
        PlayerPrefs.SetFloat("SoundVolume", soundSlider.value);
        PlayerPrefs.SetInt("soundMute", SliderFloats.soundMute);
        SliderFloats.soundValue = soundSlider.value;
    }

    // Loads the previously saved volume
    private void Load()
    {
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
    }
}
