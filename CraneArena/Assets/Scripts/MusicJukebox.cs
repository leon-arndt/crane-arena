using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playlist of tracks for the background music in the game
/// </summary>
public class MusicJukebox : MonoBehaviourSingleton<MusicJukebox>
{
    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioClip[] tracks;

    [SerializeField]
    private int currentTrackIndex = 0;

    public void Play()
    {
        source.Play();
    }

    public void NextTrack()
    {
        //increase track index and return to start if overflow
        currentTrackIndex++;
        currentTrackIndex = currentTrackIndex % tracks.Length;

        source.clip = tracks[currentTrackIndex];
        Play();
    }
}
