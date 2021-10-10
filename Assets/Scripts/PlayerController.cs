using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[System.Serializable]
public class OnPlayerCollision : UnityEvent<Collider2D, PlayerColliderType> { }

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

    [SerializeField]
    private GameObject _particleDeathPrefab;

    public OnPlayerCollision onPlayerCollision;

    [HideInInspector]
    public int _playerIndex;

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
    private bool _isOnCooldown { get; set; }
    private bool _isInvincible { get; set; }
    private bool _isDead { get; set; }
    private PlayerFaction _playerFaction { get; set; }

    private float _parryingTimeReference { get; set; }
    private float _cooldownTimeReference { get; set; }

    #endregion


    #region Unity API

    private void Start()
    {
        _currentSpeed = _speed;
        if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody2D>();
        if (_transform == null) _transform = GetComponent<Transform>();
        onPlayerCollision.AddListener(HandleOnPlayerCollision);
        _parryingTimeReference = GameManager.I.gameData.parryingTime;
        _hasParried = false;
    }

    private void FixedUpdate()
    {
        FixedMove();
    }

    #endregion


    #region Main

    public PlayerController InitPlayer(int index, PlayerFaction newFaction)
    {
        SetFaction(newFaction);
        _playerIndex = index;
        return this;
    }

    public void SetFaction(PlayerFaction faction)
    {
        _playerFaction = faction;
    }

    private void FixedMove()
    {
        Vector2 velocity = _inputMove * _currentSpeed * Time.fixedDeltaTime;
        _rigidbody.velocity = velocity;
    }

    public void OnMovement(InputAction.CallbackContext callbackContext)
    {
        if (_isParrying || _isDead)
        {
            _inputMove = Vector2.zero;
            return;
        }
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

    public IEnumerator WhileRespawning(float respawnTime)
    {
        _isDead = true;
        float elapsed = 0.0f;
        while (elapsed < respawnTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        _isDead = false;
        GameManager.I.InvokePlayerRespawn(this);
        yield break;
    }

    public void RespawnSelf(float respawnTime)
    {
        StartCoroutine(WhileRespawning(respawnTime));
    }

    public IEnumerator WhileParrying(float time, float cooldownTime)
    {
        _shield.GetComponent<Collider2D>().isTrigger = true;
        _isParrying = true;
        _isInvincible = true;
        float elapsed = 0.0f;
        while (elapsed < time)
        {
            //Debug.Log("Still in loop");
            elapsed += Time.deltaTime;
            yield return null;
        }
        _isParrying = false;
        _isInvincible = false;
        _hasParried = false;
        yield return null;
        elapsed = 0.0f;
        _isOnCooldown = true;
        DisableShield();
        while (elapsed < cooldownTime)
        {  
            yield return null;
        }
        _isOnCooldown = false;
        _shield.GetComponent<Collider2D>().isTrigger = false;
        yield break;
    }

    public void OnTriggerParry(InputAction.CallbackContext callbackContext)
    {
        if (!_isShieldUp || _isDead) return;
        StartCoroutine(WhileParrying(_parryingTimeReference, _cooldownTimeReference));
    }

    public void OnAiming(InputAction.CallbackContext callbackContext)
    {
        if (_isParrying || _isOnCooldown || _isDead) return;
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
        Instantiate(_particleDeathPrefab, _transform.position, Quaternion.identity);
        GameManager.I.onPlayerDead?.Invoke(this);
    }

    private void HandleShieldHit()
    {
        GameManager.I.onBallHitShield?.Invoke();
    }

    [SerializeField]
    private void HandleOnPlayerCollision(Collider2D collider, PlayerColliderType colliderType)
    {
        if (colliderType == PlayerColliderType.Shield && collider.gameObject.layer == 7 && _isParrying && !_hasParried)
        {
            BallController ballController = collider.gameObject.GetComponent<BallController>();
            ballController.ballRigidbody.position = _shield.transform.position + _shield.transform.up * 1.5f;
            ballController.ParryBall(_shield.transform.up);
            _hasParried = true;
        }
        if (colliderType == PlayerColliderType.Hitbox && collider.gameObject.layer == 3)
        {
            HandleDeath();
        }
        if (colliderType == PlayerColliderType.Hitbox && collider.gameObject.layer == 7 && !_isInvincible)
        {
            HandleDeath();
        }

        if (colliderType == PlayerColliderType.Shield)
        {
            HandleShieldHit();
        }
    }
    
    #endregion
}