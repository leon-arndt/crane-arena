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

    [Button]
    public void StartCountdown()
    {
        waitTime = 3f;
        StartCoroutine(CountDown());
    }

    public IEnumerator CountDown()
    {
        while (waitTime >= 0)
        {

            //decrease time
            waitTime -= Time.deltaTime;
            waitTime = Mathf.Max(0f, waitTime);

            //round two two decimal places
            waitTime = Mathf.Round(waitTime * 100f) / 100f;

            //update UI text
            uiText.text = "Next Round starts in: " + waitTime.ToString();
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
