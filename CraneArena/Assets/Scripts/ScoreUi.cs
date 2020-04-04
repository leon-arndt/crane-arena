using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// UI scene class which simply updates the scores
/// </summary>
public class ScoreUi : MonoBehaviourSingleton<ScoreUi>
{
    [SerializeField]
    private TextMeshProUGUI m_firstPlayerScore, m_secondPlayerScore;

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// Reset the score UI to default conditions
    /// </summary>
    public void Init()
    {
        UpdateScore(0, 0);
        UpdateScore(1, 0);
    }

    /// <summary>
    /// Update the score for the desired player
    /// </summary>
    /// <param name="playerId"></param>
    /// <param name="newScore"></param>
    public static void UpdateScore(int playerId, int newScore)
    {
        if (playerId == 0)
        {
            Instance.m_firstPlayerScore.text = newScore.ToString();
        }
        else if (playerId == 1)
        {
            Instance.m_secondPlayerScore.text = newScore.ToString();
        }
        else
        {
            Debug.LogError("ScoreUi: Bad player ID passed");
        }
    }
}
