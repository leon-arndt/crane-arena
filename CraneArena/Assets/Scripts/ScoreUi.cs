using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// UI scene class which updates the scores
/// </summary>
public class ScoreUi : MonoBehaviourSingleton<ScoreUi>
{
    [SerializeField]
    private TextMeshProUGUI[] scores;

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// Reset the score UI to default conditions
    /// </summary>
    public void Init()
    {
        for (int i = 0; i < scores.Length; i++)
        {
            UpdateScore(i, 0);
        }
    }

    /// <summary>
    /// Update the score for the desired player
    /// </summary>
    /// <param name="playerId"></param>
    /// <param name="newScore"></param>
    public static void UpdateScore(int playerId, int newScore)
    {
        if (playerId < Instance.scores.Length)
        {
            Instance.scores[playerId].text = newScore.ToString();
        }
        else
        {
            Debug.LogError("ScoreUi: Bad player ID passed");
        }
    }

    /// <summary>
    /// Enable or disable the desired score object (depending on active player count)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="visibility"></param>
    public static void SetScoreVisibility(int id, bool visibility)
    {
        GameObject scoreObject = Instance.scores[id].gameObject;
        scoreObject.SetActive(visibility);
    }
}
