using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

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

    [SerializeField]
    private GameObject _shield;

    public OnPlayerCollision onPlayerCollision;

    #endregion


    #region Private And Protected

    [SerializeField]
    private PlayerInput inputActions;
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private Vector2 _inputMove;
    private float _currentSpeed;
    private Vector2 _currentDirection;
    private bool _hasParried { get; set; }
    private bool _isShieldUp { get; set; }
    private bool _isParrying { get; set; }
    private PlayerFaction _playerFaction { get; set; }

    private float parryingTimeReference { get; set; }

    #endregion


    #region Unity API

    private void Start()
    {
        _currentSpeed = _speed;
        if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody2D>();
        if (_transform == null) _transform = GetComponent<Transform>();
        onPlayerCollision.AddListener(HandleOnPlayerCollision);
        parryingTimeReference = GameManager.I.gameData.parryingTime;
    }

    private void FixedUpdate()
    {
        FixedMove();
    }

    #endregion


    #region Main

    private void FixedMove()
    {
        Vector2 velocity = _inputMove * _currentSpeed * Time.fixedDeltaTime;
        _rigidbody.velocity = velocity;
    }

    public void OnMovement(InputAction.CallbackContext callbackContext)
    {
        _inputMove = callbackContext.ReadValue<Vector2>();
        _inputMove.Normalize();
    }

    private void EnableShield()
    {
        _isShieldUp = true;
        _currentSpeed = _speedWithShield;
        _shield.SetActive(true);
    }

    private void DisableShield()
    {
        _isShieldUp = false;
        _shield.SetActive(false);
        _currentSpeed = _speed;
    }

    public IEnumerator WhileParrying(float time)
    {
        _isParrying = true;
        float elapsed = 0.0f;
        while (elapsed < time)
        {
            Debug.Log("Still in loop");
            elapsed += Time.deltaTime;
            yield return null;
        }
        _isParrying = false;
        _hasParried = false;
        yield break;
    }

    public void OnTriggerParry(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Triggered");
        if (!_isShieldUp) return;
        StartCoroutine(WhileParrying(parryingTimeReference));
    }

    public void OnAiming(InputAction.CallbackContext callbackContext)
    {
        if (_isParrying) return;
        Vector2 rightStick = callbackContext.ReadValue<Vector2>();

        if(rightStick != Vector2.zero)
        {
            EnableShield();
        } else
        {
            DisableShield();
        }

        Vector3 direction = _transform.position + new Vector3(rightStick.x, rightStick.y, 0);

        _currentDirection = direction;
        _transform.LookAt(direction, Vector3.forward);

    }


    private void HandleDeath()
    {
        GameManager.I.onPlayerDead?.Invoke(this);
    }

    [SerializeField]
    private void HandleOnPlayerCollision(Collider2D collider, Collision2D collision, PlayerColliderType colliderType)
    {
        Debug.Log("" + (colliderType == PlayerColliderType.Shield) + (collision.collider.gameObject.layer == 7) + _isParrying + !_hasParried);
        if (colliderType == PlayerColliderType.Shield && collision.collider.gameObject.layer == 7 && _isParrying && !_hasParried)
        {
            Debug.Log("Parry condition fulfilled");
            collision.rigidbody.gameObject.GetComponent<BallController>().ParryBall(_currentDirection);
            _hasParried = true;
        }
        if (colliderType == PlayerColliderType.Hitbox && collision.collider.gameObject.layer == 7)
        {
            Debug.Log("Triggered death");
            HandleDeath();
        }
    }
    
    #endregion
}