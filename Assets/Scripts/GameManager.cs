using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class OnPlayerDead : UnityEvent<PlayerController>{}

public enum PlayerFaction
{
    Blue,
    Red
}

public struct PlayerInfo
{
    int index;
    PlayerController playerController;
    UnityEngine.InputSystem.PlayerInput playerInput;
}

public class GameManager : MonoBehaviour
{

    #region Exposed
    public GameData gameData;
    [HideInInspector]
    public float parryingTime;
    [HideInInspector]
    public float cooldownTime;


    #endregion
    #region Private And Protected

    private GameManager _instance;
    private bool _allPlayersJoined = false;
    private List<GameObject> players = new List<GameObject>();
    private int blueFactionScore = 0;
    private int redFactionScore = 0;
    private IEnumerator respawner;
    private SpawnerEntity[] spawners;
    private UnityEvent<PlayerController> onPlayerRespawn;

    #endregion


    #region Events

    public OnPlayerDead onPlayerDead;
    public UnityEvent onGameStart;
    public UnityEvent onRoundStart;
    public UnityEvent onBallHitShield;

    #endregion


    #region Unity API

    private void Start()
    {
        InitGameData();
        CheckForDuplicate();
        SpawnMap(0);
        blueFactionScore = 0;
        redFactionScore = 0;
        onPlayerDead.AddListener(HandleOnPlayerDead);
        onPlayerRespawn.AddListener(HandleOnPlayerRespawn);
    }

    private void Update()
    {
        if(!_allPlayersJoined && players.Count == gameData.maxPlayers)
        {
           RoundStart();
        }

        if(redFactionScore == gameData.scoreToWin || blueFactionScore == gameData.scoreToWin)
        {
            GameEnd(redFactionScore == gameData.scoreToWin ? PlayerFaction.Red : PlayerFaction.Blue);
        }
    }

    private void InitGameData()
    {
        parryingTime = gameData.parryingTime;
        cooldownTime = gameData.cooldownTime;
    }

    #endregion


    #region Main

    private void GameEnd(PlayerFaction winner)
    {

    }

    private void SpawnMap(int mapIndex)
    {
        GameObject map = gameData.maps[mapIndex];
        spawners = map.GetComponentsInChildren<SpawnerEntity>();
        Instantiate(map, Vector3.zero, Quaternion.identity);
    }

    public void RoundStart()
    {
    
    }

    public void CheckForDuplicate()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    public void InvokePlayerRespawn(PlayerController player)
    {
        onPlayerRespawn.Invoke(player);
    }

    private void HandleOnPlayerRespawn(PlayerController playerObject)
    {
        foreach (SpawnerEntity spawner in spawners)
        {
            if ((int)spawner.spawnerMode == playerObject._playerIndex)
            {
                playerObject.gameObject.transform.position = new Vector3(spawner.gameObject.transform.position.x, spawner.gameObject.transform.position.y, playerObject.gameObject.transform.position.z);
            }
        }
    }

    private void HandleOnPlayerDead(PlayerController player)
    {
        player.RespawnSelf(gameData.respawnTime);
    }

    public void OnPlayerJoin(UnityEngine.InputSystem.PlayerInput player)
    {
        player.GetComponent<PlayerController>();
        players.Add(player.gameObject);
        
    }

    #endregion


    #region SINGLETON

    private static GameManager _I;
    public static GameManager I => _I;
    public GameManager()
    {
        _I = this;
    }

    #endregion
}