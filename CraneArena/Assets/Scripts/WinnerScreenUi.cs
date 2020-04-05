using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
/// <summary>
/// Show UI information for the winner at the end of each round
/// </summary>
public class WinnerScreenUi : MonoBehaviourSingleton<WinnerScreenUi>
{
    public TextMeshProUGUI winnerText;

    [Button]
    public static void UpdateWinner(int playerId)
    {
        string winString = "";
        if (playerId == -1)
        {
            winString = "Time ran out, no winners today...";
        }
        else
        {
            winString = "Player " + playerId.ToString() + " Won The Round!";

        }
        Instance.winnerText.text = winString;
    }
}
