using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;

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

    #endregion
    #region Private And Protected

    private GameManager _instance;
    private PlayerInput[] players;

    #endregion


    #region Events

    public OnPlayerDead onPlayerDead;
    public UnityEvent onGameStart;
    public UnityEvent onRoundStart;

    #endregion


    #region Unity API

    private void Start()
    {
        InitGameData();
        CheckForDuplicate();
        onPlayerDead.AddListener(HandleOnPlayerDead);
    }

    private void InitGameData()
    {
        parryingTime = gameData.parryingTime;
    }

    #endregion


    #region Main

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

    public void OnPlayerJoin(PlayerInput test)
    {

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