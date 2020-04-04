﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private CraneMovementController m_MovementController = null;

    private float waitForRespawn = 2f;

    private Transform m_SpawnPos = null;

    public Transform SpawnPos { get => m_SpawnPos; set => m_SpawnPos = value; }

    GameObject cranePrefab = null;

    // Update is called once per frame
    void Update()
    {

    }

    internal bool CreateCrane(GameObject craneToInstantiate)
    {
        if (transform.childCount <= 0)
        {
            cranePrefab = craneToInstantiate;
                var crane = Instantiate(craneToInstantiate, transform);
            SetupCraneComponents(crane);
            return true;
        }
        return false;
    }

    private void SetupCraneComponents(GameObject crane)
    {
        m_MovementController = GetComponent<CraneMovementController>();
        m_MovementController.SetupComponents();
    }

    internal void InLoseZone()
    {
        //disable movement
        m_MovementController.CanMove = false;

        StartCoroutine(RespawnPlayer(waitForRespawn));
    }

    private IEnumerator RespawnPlayer(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);


        transform.position = m_SpawnPos.position;
        transform.rotation = m_SpawnPos.rotation;
        m_MovementController.CanMove = true;
    }

    internal void Respawn()
    {

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        var crane = Instantiate(cranePrefab, transform);
        SetupCraneComponents(crane);
        m_MovementController.CanMove = true;
        Debug.Log("Respawn!");
    }
}