using UnityEngine;
using UnityEngine.Events;

public enum PlayerColliderType
{
    Hitbox,
    Shield,
}

[RequireComponent(typeof(Collider2D))]
public class PlayerColliderController : MonoBehaviour
{
    #region Exposed

    public PlayerColliderType playerColliderType;

    [SerializeField]
    private PlayerController playerMovement;
    #endregion


    #region Private And Protected

    #endregion


    #region Unity API
    private void OnTriggerEnter2D(Collider2D collider)
    {
        playerMovement.onPlayerCollision.Invoke(collider, playerColliderType);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerMovement.onPlayerCollision?.Invoke(collision.collider, playerColliderType);
    }

    #endregion
}