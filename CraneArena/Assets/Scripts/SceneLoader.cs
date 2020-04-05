using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Utility class to reload the game scene
/// </summary>
public class SceneLoader : MonoBehaviourSingleton<SceneLoader>
{
    public void DelayedReload()
    {
        Invoke("Reload", 5f);
    }

    public void Reload()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
