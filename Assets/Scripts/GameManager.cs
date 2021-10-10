using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;

[System.Serializable]
public class OnPlayerDead : UnityEvent<PlayerController>{}

[System.Serializable]
public class OnBallHitShield : UnityEvent{}

public class GameManager : MonoBehaviour
{
    #region Private And Protected

    private GameManager _instance;

    #endregion


    #region Events

    public OnPlayerDead onPlayerDead;
    public OnBallHitShield OnBallHitShield;

    #endregion


    #region Unity API

    private void Start()
    {
        CheckForDuplicate();
        onPlayerDead.AddListener(HandleOnPlayerDead);
    }

    private void Instance_onPlayerJoined(UnityEngine.InputSystem.PlayerInput obj)
    {
        throw new System.NotImplementedException();
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

    private void HandleOnBallHitShield()
    {
        Debug.Log("hit shield");
    }

    public Action<PlayerInput> OnPlayerJoined(Action<PlayerInput> playerInput)
    {
        return null;
    }

    public void OnPlayerJoin(PlayerInput test)
    {

    }

    public void onjoin()
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