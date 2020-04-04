using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public float timeLeft;

    [SerializeField] private Transform[] m_SpawnPositions;
    [SerializeField] private GameObject[] m_CorrespondingCranesToSpawn;

    private int currentindex = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Spawn Players
        var inputManager = GetComponent<PlayerInputManager>();
        if (!inputManager) { return; }
        var player = inputManager.playerPrefab;
        for (int i = 0; i < m_SpawnPositions.Length; i++)
        {

        }

        timeLeft = 90f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            //Game over
            Debug.Log("Time ran out");
            timeLeft = 0f;
        }
    }

    public void OnPlayerJoined()
    {
        var players = FindObjectsOfType<PlayerManager>();
        foreach (var player in players)
        {
            //setup joined player
            if (player.CreateCrane(m_CorrespondingCranesToSpawn[currentindex]))
            {
                player.SpawnPos = m_SpawnPositions[currentindex];
                player.transform.position = player.SpawnPos.position;
                player.transform.rotation = player.SpawnPos.rotation;
                currentindex++;
            }
        }
    }

}
