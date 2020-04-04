using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle all sound for the game
/// Dynamically creates audio sources from audio clips
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private const float safetyTailDuration = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        //Set up singleton
        Instance = this;
    }

    //Create audio source as child in scene and destroy after clip duration
    public static void CreateTemporarySound(AudioClip clip)
    {
        AudioSource source = CreateSound(clip);
        Destroy(source.gameObject, clip.length + safetyTailDuration);
    }

    //Create audio source (with random pitch) as child in scene and destroy after clip duration
    public static void CreateTemporarySoundRandomPitch(AudioClip clip, float minPitch = 0.5f, float maxPitch = 1.5f)
    {
        AudioSource source = CreateSound(clip);
        Destroy(source.gameObject, clip.length + safetyTailDuration);
        source.pitch = Random.Range(minPitch, maxPitch);

        Destroy(source.gameObject, clip.length + safetyTailDuration);
    }

    public static AudioSource CreateSound(AudioClip clip)
    {
        GameObject audioGO = new GameObject(clip.name);
        AudioSource source = audioGO.AddComponent<AudioSource>();
        source.clip = clip;
        source.Play();
        audioGO.transform.SetParent(AudioManager.Instance.transform);

        return source;
    }
}
