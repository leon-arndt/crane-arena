using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage the deleting of players when the round starts
/// </summary>
public class PlayerHolder : MonoBehaviourSingleton<PlayerHolder>
{
    public Transform holder;

    public static void DeleteAll()
    {
        foreach (Transform child in Instance.holder)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
