using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

/// <summary>
/// Let the map shrink after time when the game is about to end
/// </summary>
public class MapShrinker : MonoBehaviourSingleton<MapShrinker>
{
    bool m_isShrinking;
    Vector3 standardScale;

    private void Start()
    {
        standardScale = transform.localScale;    
    }

    [Button]
    public void StartShrinking(float duration)
    {
        //exit early
        if (m_isShrinking) return;

        m_isShrinking = true;
        transform.DOScale(0.2f, duration);
    }

    /// <summary>
    /// Return to the normal scaling settings
    /// </summary>
    [Button]
    public static void ResetScale()
    {
        Instance.m_isShrinking = false;
        Instance.transform.DOKill();
        Instance.transform.localScale = Instance.standardScale;
    }
}
