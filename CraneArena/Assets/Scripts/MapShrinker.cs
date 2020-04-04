using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    public void StartShrinking(float duration)
    {
        m_isShrinking = true;
        transform.DOScale(0.2f, duration);
    }

    public static void ResetScale()
    {
        Instance.transform.DOKill();
        Instance.transform.localScale = Instance.standardScale;
    }
}
