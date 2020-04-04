using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player logic, handles death conditions
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float waitForRespawn = 2f;

    //TODO! Make this player id dynamic for each player
    private int m_playerId = 0;

    private CraneMovementController m_MovementController = null;
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

        //update score
        ScoreTracker.IncreaseOtherPlayer(m_playerId);

        StartCoroutine(RespawnPlayer(waitForRespawn));
    }

    private IEnumerator RespawnPlayer(float timeToWait)
    {
        
        yield return new WaitForSeconds(timeToWait);
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        yield return new WaitForFixedUpdate();
        var crane = Instantiate(cranePrefab, transform);
        SetupCraneComponents(crane);
        m_MovementController.CanMove = true;
    }

    internal void Respawn()
    {
        m_MovementController.CanMove = false;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        StartCoroutine(RespawnPlayer(0.5f));
       
        Debug.Log("Respawn!");
    }
}
