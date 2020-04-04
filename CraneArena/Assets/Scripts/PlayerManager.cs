using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player logic, handles death conditions
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject m_ReadyIndicator = null;
    [SerializeField] private float waitForRespawn = 2f;

    private Transform m_SpawnPos = null;
    public Transform SpawnPos { get => m_SpawnPos; set => m_SpawnPos = value; }

    private bool m_IsReadyToStart;
    public bool IsReadyToStart { get => m_IsReadyToStart; set => m_IsReadyToStart = value; }

    private int m_Score = 0;
    public int Score { get => m_Score; set => m_Score = value; }

    private CraneMovementController m_MovementController = null;


    GameObject cranePrefab = null;

    public static event Action<PlayerManager> onPlayerDeath;
    public static event Action<PlayerManager> onPlayerReady;

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

        //player dead

        onPlayerDeath(this);

        //StartCoroutine(RespawnPlayer(waitForRespawn));

    }

    private IEnumerator RespawnPlayer(float timeToWait)
    {
        transform.DetachChildren();

        yield return new WaitForSeconds(timeToWait);

        yield return new WaitForFixedUpdate();
        var crane = Instantiate(cranePrefab, transform);
        SetupCraneComponents(crane);
        m_MovementController.CanMove = true;
    }

    internal void Respawn()
    {
        m_MovementController.CanMove = false;

        StartCoroutine(RespawnPlayer(0.5f));

        Debug.Log("Respawn!");
    }

    public void OnJump()
    {
        if (GameManager.Instance.HasStarted) { return; }
        IsReadyToStart = true;
        //ready event
        onPlayerReady(this);
        //show ready confirmation

    }

    public void SpawnIndicator(){
        GameObject indicator = Instantiate(m_ReadyIndicator, transform);

        //setup indicator
        CraneMainBody mainBody = GetComponentInChildren<CraneMainBody>();
        indicator.GetComponent<ReadyIndicator>().Own(mainBody.transform);
    }
}
