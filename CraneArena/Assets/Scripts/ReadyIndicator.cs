using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scene object which indicates that the player is ready
/// </summary>
public class ReadyIndicator : MonoBehaviour
{
    public static List<ReadyIndicator> indicators;

    // Start is called before the first frame update
    void Start()
    {
        indicators.Add(this);
    }

    public static void DestroyAll()
    {
        foreach (var item in indicators)
        {
            Destroy(item.gameObject);
        }
    }
}
