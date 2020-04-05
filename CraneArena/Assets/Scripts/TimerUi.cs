using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
    
public class TimerUi : MonoBehaviourSingleton<TimerUi>
{
    [SerializeField]
    private TextMeshProUGUI m_timerText;

    // Update is called once per frame
    void Update()
    {
        string minutesAndSeconds = ConvertFloatToMinutesSeconds(GameManager.Instance.roundTimeLeft);
        m_timerText.text = minutesAndSeconds;
    }

    public static string ConvertFloatToMinutesSeconds(float f)
    {
        string subTenAddendum = f % 60 < 10 ? "0" : "";
        return ((int)(f / 60f)).ToString() + ":" + subTenAddendum + ((int)(f % 60)).ToString();
    }
}
