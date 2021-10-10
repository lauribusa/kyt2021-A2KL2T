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
    private bool _allPlayersJoined;
    private List<UnityEngine.InputSystem.PlayerInput> players;

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
        onPlayerDead.AddListener(HandleOnPlayerDead);
        SpawnMap(0);
    }

    private void Update()
    {
        if(!_allPlayersJoined && players.Count == gameData.maxPlayers)
        {
            RoundStart();
        }
    }

    private void InitGameData()
    {
        parryingTime = gameData.parryingTime;
        cooldownTime = gameData.cooldownTime;
    }

    #endregion


    #region Main

    private void SpawnMap(int mapIndex)
    {
        GameObject map = gameData.maps[mapIndex];
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

    private void HandleOnPlayerDead(PlayerController player)
    {
        Debug.Log("Player is dead: " + player);
        Destroy(player.gameObject);
    }

    public void OnPlayerJoin(UnityEngine.InputSystem.PlayerInput player)
    {
        Debug.Log(player.playerIndex);
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