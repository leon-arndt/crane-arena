using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scene object which indicates that the player is ready
/// </summary>
public class ReadyIndicator : MonoBehaviour
{
    Transform playerOwner;

    public static List<ReadyIndicator> indicators = new List<ReadyIndicator>();

    // Start is called before the first frame update
    void Start()
    {
        indicators.Add(this);

        //play feedback sound
        SoundPlayer.Play(SoundEventEnum.PositiveFeedbackShort);
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.position = playerOwner.position + 2f * Vector3.up;
    }
    public static void DestroyAll()
    {
        foreach (var item in indicators)
        {
            Destroy(item.gameObject);
        }
    }

    public void Own(Transform newOwner)
    {
        playerOwner = newOwner;
    }
}
