using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public float timeLeft;

    [SerializeField] private Transform[] m_SpawnPositions = null;
    [SerializeField] private GameObject[] m_CorrespondingCranesToSpawn = null;
    private List<PlayerManager> m_Players = new List<PlayerManager>();
    private List<PlayerManager> playersAlive = new List<PlayerManager>();
    private List<PlayerManager> playersReady = new List<PlayerManager>();

    private int currentIndex = 0;
    private bool m_HasStarted = false;

    public bool HasStarted { get => m_HasStarted; }

    // Start is called before the first frame update
    void Start()
    {
        //Spawn Players
        //         var inputManager = GetComponent<PlayerInputManager>();
        //         if (!inputManager) { return; }
        //         var player = inputManager.playerPrefab;
        //         for (int i = 0; i < m_SpawnPositions.Length; i++)
        //         {
        // 
        //         }

        timeLeft = 90f;
    }
    private void OnEnable()
    {
        PlayerManager.onPlayerDeath += OnPlayerDeath;
        PlayerManager.onPlayerReady += OnPlayerReady;
    }

    private void OnDisable()
    {
        PlayerManager.onPlayerDeath -= OnPlayerDeath;
        PlayerManager.onPlayerReady -= OnPlayerReady;
    }
    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0 && m_HasStarted)
        {
            //Game Start
            timeLeft -= Time.deltaTime;
            StartRound();
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
            if (currentIndex < m_CorrespondingCranesToSpawn.Length && player.CreateCrane(m_CorrespondingCranesToSpawn[currentIndex]))
            {
                player.SpawnPos = m_SpawnPositions[currentIndex];
                player.transform.position = player.SpawnPos.position;
                player.transform.rotation = player.SpawnPos.rotation;
                m_Players.Add(player);

                // Enable UI Elements
                ScoreUi.SetScoreVisibility(currentIndex, true);


                currentIndex++;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void StartRound()
    {
        playersAlive.Clear();
        foreach (var player in m_Players)
        {
            playersAlive.Add(player);
        }

        CanvasGroupSwitcher.ShowGamePanel();
        foreach (var player in m_Players)
        {
            Destroy(player.transform.GetChild(0).gameObject);
            player.Respawn();
        }
        Debug.Log("Started Round");
    }

    /// <summary>
    /// Gets called when player dies
    /// </summary>
    public void OnPlayerDeath(PlayerManager player)
    {
        //set player dead
        playersAlive.Remove(player);

        //check if all player dead
        if (playersAlive.Count == 1)
        {
            //notify winner
            var playerId = m_Players.IndexOf(playersAlive[0]);
            Debug.Log("Winner: "+playerId);
            ScoreTracker.IncreaseScoreByOne(playerId);


            //#TODO: Destroy Player Crane
            StartCoroutine(StartRoundAfterDelay(0.1f));//#TODO: add delay with animations
        }
    }

    private IEnumerator StartRoundAfterDelay(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        StartRound();
    }

    public void OnPlayerReady(PlayerManager player)
    {
        if (playersReady.Contains(player)) { return; }

        player.SpawnIndicator();
        playersReady.Add(player);

        if (playersReady.Count != m_Players.Count) { return; }

        StartRound();
        

    }
}
