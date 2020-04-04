using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

/// <summary>
/// Switch between the different canvas groups in the game such as the game, winner, and intro screen
/// </summary>
public class CanvasGroupSwitcher : MonoBehaviourSingleton<CanvasGroupSwitcher>
{
    [SerializeField]
    CanvasGroup[] m_canvasGroups;

    [SerializeField]
    CanvasGroup m_defaultCanvasGroup;

    [SerializeField]
    CanvasGroup m_GameCanvasGroup;

    [SerializeField]
    CanvasGroup m_winnerCanvasGroup;

    private void Start()
    {
        OpenDefaultPanel(); 
    }

    private void OpenDefaultPanel()
    {
        SetActiveGroup(m_defaultCanvasGroup.name);
    }

    /// <summary>
    /// Please see the complete documentary for this function on www.cranearena.io
    /// Sets the active canvas group, goes through each canvas group in the canvas group array
    /// </summary>
    /// <param name="targetName">The name of the desired canvas group</param>
    /// <param name="fadeTime">The duration of the fade</param>
    public static void SetActiveGroup(string targetName, float fadeTime = 0.2f)
    {
        //Instance.m_gameGroup.gameObject.SetActive(Instance.m_gameGroup.name.Equals(targetName));

        float targetAlpha = 0f;

        foreach (var item in Instance.m_canvasGroups)
        {
            targetAlpha = item.name.Equals(targetName) ? 1f : 0f;
            item.DOFade(targetAlpha, fadeTime);
        }
    }

    internal static void ShowGamePanel()
    {
        SetActiveGroup(Instance.m_GameCanvasGroup.name);
    }

    internal static void ShowWinnerPanel()
    {
        SetActiveGroup(Instance.m_winnerCanvasGroup.name);
    }
}
