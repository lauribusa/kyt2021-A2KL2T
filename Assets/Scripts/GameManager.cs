using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnPlayerDead : UnityEvent<PlayerController>{}

public class GameManager : MonoBehaviour
{
    #region Private And Protected

    private GameManager _instance;


    #endregion


    #region Events

    public OnPlayerDead onPlayerDead;

    #endregion


    #region Unity API

    private void Start()
    {
        CheckForDuplicate();
        onPlayerDead.AddListener(HandleOnPlayerDead);
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