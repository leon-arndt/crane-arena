using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles various aspects of the game including the communication of rounds and game states
/// Reacts to events such as player deaths
/// </summary>
public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public float roundTimeLeft;

    [SerializeField] private Transform[] m_SpawnPositions = null;
    [SerializeField] private GameObject[] m_CorrespondingCranesToSpawn = null;
    private List<PlayerManager> m_Players = new List<PlayerManager>();
    private List<PlayerManager> playersAlive = new List<PlayerManager>();
    private List<PlayerManager> playersReady = new List<PlayerManager>();

    private int currentIndex = 0;
    private bool m_HasStarted = false;

    public bool HasStarted { get => m_HasStarted; }
    public bool roundOver;

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
        if (roundTimeLeft > 0 && m_HasStarted)
        {
            //Game has started and time should count down
            roundTimeLeft -= Time.deltaTime;

            //determine whether to start shrinking
            float shrinkCountDown = 20f;
            if (roundTimeLeft < shrinkCountDown)
            {
                MapShrinker.Instance.StartShrinking(shrinkCountDown);
            }
        }
        else if (m_HasStarted && !roundOver && roundTimeLeft <= 0)
        {
            //Game over through lack of time
            Debug.Log("Time ran out");
            roundTimeLeft = 0f;
            roundOver = true;

            CanvasGroupSwitcher.ShowWinnerPanel();
            WinnerScreenUi.UpdateWinner(-1);

            //restart round after delay
            StartCoroutine(StartRoundAfterDelay(4f));
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
    /// Start each round and reset relevant aspects of the game
    /// </summary>
    public void StartRound()
    {
        //start time
        roundTimeLeft = 45f;

        playersAlive.Clear();
        foreach (var player in m_Players)
        {
            playersAlive.Add(player);
        }
        CanvasGroupSwitcher.ShowGamePanel();
        foreach (var player in m_Players)
        {
            if (HasStarted)
            {
                player.transform.GetChild(0).transform.parent = null;
            }
            else
            {
                //lobby pregame
                player.transform.GetChild(0).transform.parent = PlayerHolder.Instance.holder;
            }
            player.Respawn();
        }

        //Reset map scaling
        MapShrinker.ResetScale();

        //music
        MusicJukebox.Instance.NextTrack();

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

            //User interface update
            WinnerScreenUi.UpdateWinner(playerId);
            CanvasGroupSwitcher.ShowWinnerPanel();

            //winner sound effect
            SoundPlayer.Play(SoundEventEnum.RoundWin);

            //#TODO: Destroy Player Crane
            StartCoroutine(StartRoundAfterDelay(4f));//#TODO: add delay with animations
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

        StartGame(player);
    }


    public void StartGame(PlayerManager player)
    {
        player.SpawnIndicator();
        playersReady.Add(player);

        if (playersReady.Count != m_Players.Count) { return; }

        //Start Game
        StartRound();
        m_HasStarted = true;

        //destroy all indicators when the game begins
        ReadyIndicator[] indicators = FindObjectsOfType<ReadyIndicator>();
        foreach (var item in indicators)
        {
            Destroy(item.gameObject);
        }

        //destroy all lobby players
        PlayerHolder.DeleteAll();

        //start spawning hazards
        HazardSpawner.Instance.StartSpawning();
    }
}
