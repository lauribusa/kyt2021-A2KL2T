using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
    public bool isPaused;


    #endregion
    #region Private And Protected

    [SerializeField]
    private GameObject _ballPrefab;
    private GameManager _instance;
    private bool _allPlayersJoined = false;
    private bool _roundStarted = false;
    private List<GameObject> players = new List<GameObject>();
    private int blueFactionScore = 0;
    private int redFactionScore = 0;
    private IEnumerator respawner;
    private SpawnerEntity[] spawners;
    private int _currentMapIndex = 0;
    private GameObject _currentMap;
    private UnityEvent<PlayerController> onPlayerRespawn = new UnityEvent<PlayerController>();

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
        if(!_allPlayersJoined && players.Count == gameData.maxPlayers && !_roundStarted)
        {
           RoundStart();

        }

        if(redFactionScore == gameData.scoreToWin || blueFactionScore == gameData.scoreToWin)
        {
            if(_currentMapIndex >= gameData.maps.Length)
            {
                GameEnd(redFactionScore == gameData.scoreToWin ? PlayerFaction.Red : PlayerFaction.Blue);
            }
            _currentMapIndex++;
            
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
        // load winner screen
        Gamepad.current.aButton.ReadValue();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SpawnMap(int mapIndex)
    {
        _currentMap = gameData.maps[mapIndex];
        spawners = _currentMap.GetComponentsInChildren<SpawnerEntity>();
        Instantiate(_currentMap, new Vector3(0,0,1), Quaternion.identity);
    }

    public IEnumerator LaunchingBall(float launchTimer)
    {
        float elapsed = 0.0f;
        while(elapsed < launchTimer)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
     
        yield break;
    }

    public void LaunchBall(SpawnerEntity ballSpawner)
    {
        Instantiate(_ballPrefab, ballSpawner.transform.position, Quaternion.identity);
    }

    public void RoundStart()
    {
        SpawnMap(_currentMapIndex);
        foreach (var player in players)
        {
            PlayerController playerObject = player.GetComponent<PlayerController>();
            foreach (var spawner in spawners)
            {
                if ((int)spawner.spawnerMode == playerObject._playerIndex)
                {
                    player.transform.position = new Vector3(spawner.gameObject.transform.position.x, spawner.gameObject.transform.position.y, player.transform.position.z);
                }
            }
        }
        _roundStarted = true;
        isPaused = false;
        foreach (SpawnerEntity spawner in spawners)
        {
            if (spawner.spawnerMode == SpawnerMode.Ball)
            {
                LaunchBall(spawner);
            }
        }
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
        if (player._playerFaction == PlayerFaction.Blue)
        {
            redFactionScore++;
        }
        if (player._playerFaction == PlayerFaction.Red)
        {
            blueFactionScore++;
        }
        player.RespawnSelf(gameData.respawnTime);
    }

    public void OnPlayerJoin(UnityEngine.InputSystem.PlayerInput player)
    {
        PlayerController currentPlayer = player.GetComponent<PlayerController>();
        players.Add(player.gameObject);
        HandleOnPlayerRespawn(currentPlayer);
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