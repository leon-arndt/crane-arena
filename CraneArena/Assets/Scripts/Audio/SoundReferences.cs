using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Holds references in scene
/// Uses a dictionary data object to link a public EventName enum with audioclips
/// </summary>
public class SoundReferences : SerializedMonoBehaviour
{
    public static SoundReferences Instance;

    public SoundDictionary soundDictionary;

    private void Start()
    {
        Instance = this;    
    }
}
