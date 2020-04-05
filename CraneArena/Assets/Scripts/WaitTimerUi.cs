using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

/// <summary>
/// Used to display the time between rounds
/// </summary>
public class WaitTimerUi : MonoBehaviourSingleton<WaitTimerUi>
{
    public TextMeshProUGUI uiText;
    public float waitTime;

    private void Update()
    {
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            waitTime = Mathf.Max(0f, waitTime);

            //round two two decimal places
            waitTime = Mathf.Round(waitTime * 100f) / 100f;

            //update UI text and format string properly
            uiText.text = "Next Round starts in: " + waitTime.ToString("0.00");
        }
    }

    [Button]
    public void StartCountdown()
    {
        waitTime = 4f;
    }
}
