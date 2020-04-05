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
        string winString = "Player " + playerId.ToString() + " wins the round!";
        Instance.winnerText.text = winString;
    }
}
