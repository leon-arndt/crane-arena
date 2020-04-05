using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General score tracker class.
/// Works together with ScoreUi when the score is changed
/// </summary>
public class ScoreTracker : MonoBehaviourSingleton<ScoreTracker>
{
    [SerializeField]
    private int m_player1Score, m_player2Score, m_player3Score, m_player4Score;

    // Start is called before the first frame update
    void Start()
    {
        ResetScores();
    }

    /// <summary>
    /// Reset all player scores to zero
    /// </summary>
    private void ResetScores()
    {
        m_player1Score = 0;
        m_player2Score = 0;
        m_player3Score = 0;
        m_player4Score = 0;
    }

    /// <summary>
    /// Increase the desired player score by one
    /// Could already be refactored into two individual playerScore classes with fields
    /// </summary>
    /// <param name="playerId">The index of the player whose scores should be increased</param>
    public static void IncreaseScoreByOne(int playerId)
    {
        if (playerId == 0)
        {
            Instance.m_player1Score++;
            ScoreUi.UpdateScore(playerId, Instance.m_player1Score);
        }
        else if (playerId == 1)
        {
            Instance.m_player2Score++;
            ScoreUi.UpdateScore(playerId, Instance.m_player2Score);
        }
        else if (playerId == 2)
        {
            Instance.m_player3Score++;
            ScoreUi.UpdateScore(playerId, Instance.m_player3Score);
        }
        else if (playerId == 3)
        {
            Instance.m_player4Score++;
            ScoreUi.UpdateScore(playerId, Instance.m_player4Score);
        }
        else
        {
            Debug.LogError("ScoreTracker: Bad player ID passed");
        }
    }
}
