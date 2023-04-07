using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SliderScriptableObject", menuName = "ScriptableObjects/Sliders")] 
public class SliderValues : ScriptableObject
{
    // value for music slider
    public float volumeValue;

    // value for sound slider;
    public float soundValue;

    public int volumeMute;
    public int soundMute;

    public void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
}
