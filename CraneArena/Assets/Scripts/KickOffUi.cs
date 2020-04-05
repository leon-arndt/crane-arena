using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KickOffUi : MonoBehaviourSingleton<KickOffUi>
{
    public CanvasGroup group;

    public void ShowKickOffText()
    {
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        //show text
        group.DOFade(1f, 0.2f);
        yield return new WaitForSeconds(1f);

        //hide again
        group.DOFade(0f, 0.5f);
    }
}
