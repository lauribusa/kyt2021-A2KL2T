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
    private Collider2D playerCollider;
    [SerializeField]
    private PlayerController playerMovement;
	#endregion

	
	#region Private And Protected
   	
	#endregion

	
	#region Unity API

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerMovement.onPlayerCollision.Invoke(playerCollider, collision, playerColliderType);
    }

    #endregion

}

