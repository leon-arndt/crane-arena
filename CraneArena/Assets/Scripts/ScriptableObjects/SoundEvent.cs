using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "SoundEvent", menuName = "ScriptableObjects/SoundEvent", order = 1)]
public class SoundEvent : ScriptableObject
{
    //Make sure that the eventName is only updated automatically
    [ReadOnly]
    public string eventName;

    public AudioClip clip;

    [Range(0, 1)]
    public float volume;

   public void OnValidate()
    {
        FileNameToNameField();
    }

    /// <summary>
    /// Automatically update the eventName to the fileName to avoid errors and naming effor
    /// </summary>
    public void FileNameToNameField()
    {
        string assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
        eventName = Path.GetFileNameWithoutExtension(assetPath);
    }
}
