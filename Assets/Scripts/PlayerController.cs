using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnPlayerCollision : UnityEvent<Collider2D, Collision2D, PlayerColliderType> { }

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
	#region Exposed

    [SerializeField]
    private float _speed = 10;

    [SerializeField]
    private float _speedWithShield = 5;

    public OnPlayerCollision onPlayerCollision;
	
	#endregion
	
	
   	#region Private And Protected

    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private Vector2 _inputMove;
   	
   	#endregion
	
	
	#region Unity API
	
    private void Start()
    {
        if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody2D>();
        if (_transform == null) _transform = GetComponent<Transform>();
        onPlayerCollision.AddListener(HandleOnPlayerCollision);
    }

    private void Update()
    {
        SetupMovement();
        SetupDirection();
    }

    private void FixedUpdate()
    {
        FixedMove();
    }

    #endregion
    
    
    #region Main

    private void FixedMove()
    {
        Vector2 velocity = _inputMove * _speed * Time.fixedDeltaTime;
        _rigidbody.velocity = velocity;
    }

    private void SetupMovement()
    {
        float horizontal = Input.GetAxisRaw("HorizontalMove");
        float vertical = Input.GetAxisRaw("VerticalMove");

        _inputMove = new Vector2(horizontal, vertical);
        _inputMove.Normalize();
    }

    private void SetupDirection()
    {
        float horizontal = Input.GetAxisRaw("HorizontalOrientation");
        float vertical = Input.GetAxisRaw("VerticalOrientation");

        Vector3 direction = _transform.position + new Vector3(horizontal, vertical, 0);
        
        _transform.LookAt(direction, Vector3.forward);
    }

    private void HandleDeath()
    {
        GameManager.I.onPlayerDead?.Invoke(this);
    }

    [SerializeField]
    private void HandleOnPlayerCollision(Collider2D collider, Collision2D collision, PlayerColliderType colliderType)
    {
        Debug.Log("Collision Detected: "+colliderType.ToString());

        if (colliderType == PlayerColliderType.Hitbox)
        {
            HandleDeath();
        }
    }
    
    #endregion
}