using System.Collections;
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
    private bool _isParrying;

    #endregion


    #region Unity API

    private void Start()
    {
        _currentSpeed = _speed;
        if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody2D>();
        if (_transform == null) _transform = GetComponent<Transform>();
        onPlayerCollision.AddListener(HandleOnPlayerCollision);
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
        _currentSpeed = _speedWithShield;
        _shield.SetActive(true);
    }

    private void DisableShield()
    {
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
        yield break;
    }

    private void OnTriggerParry(InputAction.CallbackContext callbackContext)
    {

    }

    public void OnAiming(InputAction.CallbackContext callbackContext)
    {
        Vector2 rightStick = callbackContext.ReadValue<Vector2>();

        if(rightStick != Vector2.zero)
        {
            EnableShield();
        } else
        {
            DisableShield();
        }

        Vector3 direction = _transform.position + new Vector3(rightStick.x, rightStick.y, 0);

        _transform.LookAt(direction, Vector3.forward);

    }


    private void HandleDeath()
    {
        GameManager.I.onPlayerDead?.Invoke(this);
    }

    [SerializeField]
    private void HandleOnPlayerCollision(Collider2D collider, Collision2D collision, PlayerColliderType colliderType)
    {
        if (colliderType == PlayerColliderType.Hitbox && collision.collider.gameObject.layer == 7)
        {
            HandleDeath();
        }
    }
    
    #endregion
}