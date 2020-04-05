using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
/// <summary>
/// Show UI information for the winner at the end of each round
/// </summary>
public class EndScreenUi : MonoBehaviourSingleton<EndScreenUi>
{
    public TextMeshProUGUI winnerText;

    [Button]
    public static void UpdateWinner(int playerId)
    {
        string winString = "";

        {
            //increase by 1, since real humans begin counting with 1
            playerId++;
            winString = "Player " + playerId.ToString() + " Won the Game!";
        }
        Instance.winnerText.text = winString;
    }
}
