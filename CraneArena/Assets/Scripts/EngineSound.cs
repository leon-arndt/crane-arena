using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle the dynamic engine sounds
/// </summary>
public class EngineSound : MonoBehaviour
{
    public AudioSource engineSource;
    public float overrideVolumeMult = 1f;
    public bool muted;

    private void Update()
    {
        if (muted) return;

        if (transform.position.y < -2f)
        {
            muted = true;
            overrideVolumeMult = 0f;
            engineSource.volume = 0f;
        }
    }
    public void UpdateEngineSound(float speed)
    {
        if (muted) return;

        //dynamic engine sounds
        engineSource.pitch = 0.3f + 0.15f * speed;
        engineSource.volume = overrideVolumeMult * 0.2f + 0.1f * speed;
    }
}
