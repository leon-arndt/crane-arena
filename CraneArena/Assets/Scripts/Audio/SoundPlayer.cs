using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

/// <summary>
/// Addresses the abstract audio managers with various implemented methods
/// SoundPlayer can be used from anywhere using static functions
/// </summary>
public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Fire test audio event");
            Play(SoundEventEnum.Test);
        }
    }

    /// <summary>
    /// Play sound using AudioManager and SoundReferences dictionary
    /// </summary>
    /// <param name="eventEnum enum"></param>
    public static void Play(SoundEventEnum eventEnum)
    {
        //Find value in dictionary
        SoundDictionary dictionary = SoundReferences.Instance.soundDictionary;
        AudioClip sound = dictionary.references.Single(s => s.Key == eventEnum).Value;
        AudioManager.CreateTemporarySound(sound);
    }

    /// <summary>
    /// Similar to the regular Play method but can pass random pitch
    /// </summary>
    /// <param name="eventEnum"></param>
    /// <param name="minPitch"></param>
    /// <param name=""></param>
    public static void PlayRandomPitch(SoundEventEnum eventEnum, float minPitch, float floatMaxPitch)
    {
        //Find value in dictionary
        SoundDictionary dictionary = SoundReferences.Instance.soundDictionary;
        AudioClip sound = dictionary.references.Single(s => s.Key == eventEnum).Value;
        AudioManager.CreateTemporarySoundRandomPitch(sound);
    }
}
