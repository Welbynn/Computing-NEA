using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextScriptableObject", menuName = "ScriptableObjects/CompleteText")]
public class CompleteText : ScriptableObject
{
    public bool[] levelTexts = {false, false, false};

    public void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
}
