using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Public Enum of event names which can be accessed by any class
/// </summary>
public enum SoundEventEnum
{
    Test,
    Idont,
    even,
    know
}

[CreateAssetMenu(fileName = "SoundDictionary", menuName = "ScriptableObjects/SoundDictionary", order = 1)]
public class SoundDictionary : SerializedScriptableObject
{
    // The public dictionary to hold the pairings
    public Dictionary<SoundEvent, AudioClip> references;
}
