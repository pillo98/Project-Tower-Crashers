using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [Header("Ground Check Properties")]
    [SerializeField]
    private float _groundCheckHeight;
    [SerializeField]
    private LayerMask _groundMask;
    [SerializeField]
    private float _disableGCTime;

    private Vector2 _boxCenter;
    private Vector2 _boxSize;
    private bool _jumping;
    private float _initialGravityScale;
    private bool _groundCheckEnabled = true;
    private WaitForSeconds _wait;

    // Movement
    [SerializeField]
    private float _speed;
    private PlayerActions _playerActions;
    private Rigidbody2D _rb;
    private Vector2 _moveInput;

    // Jump
    [SerializeField]
    private float _jumpPower;
    [SerializeField]
    [Range(1f, 5f)]
    private float _jumpFallGravityMultiplier;
    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _playerActions = new PlayerActions();

        _rb = GetComponent<Rigidbody2D>();
        if (_rb is null)
            Debug.LogError("Rigidbody2D is NULL!");

        _initialGravityScale = _rb.gravityScale;

        _boxCollider = GetComponent<BoxCollider2D>();
        if (_boxCollider is null)
            Debug.Log("BoxCollider2D is NULL!");

        _wait = new WaitForSeconds(_disableGCTime);

        _playerActions.Player_Map.Jump.performed += Jump_Performed;
    }

    private void OnEnable()
    {
        _playerActions.Player_Map.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Player_Map.Disable();
    }

    private void Jump_Performed(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            _rb.velocity += Vector2.up * _jumpPower;
            _jumping = true;
            StartCoroutine(EnableGroundCheckAfterJump());
        }
    }

    private bool IsGrounded()
    {
        // Center coordinate of box that checks if the player is touching the ground
        _boxCenter = new Vector2(_boxCollider.bounds.center.x, _boxCollider.bounds.center.y) +
            (Vector2.down * (_boxCollider.bounds.extents.y + (_groundCheckHeight / 2f)));

        // Size of the box (width, height)
        _boxSize = new Vector2(_boxCollider.bounds.size.x, _groundCheckHeight);

        var groundBox = Physics2D.OverlapBox(_boxCenter, _boxSize, 0f, _groundMask);

        if (groundBox != null)
            //Debug.Log("Collider is not null");
            return true;
        return false;
    }

    private IEnumerator EnableGroundCheckAfterJump()
    {
        _groundCheckEnabled = false;
        yield return _wait;
        _groundCheckEnabled = true;
    }

    private void HandleGravity()
    {
        if (_groundCheckEnabled && IsGrounded())
        {
            _jumping = false;
        }
        else if (_jumping && _rb.velocity.y < 0f) // jump fall 
        {
            _rb.gravityScale = _initialGravityScale * _jumpFallGravityMultiplier;
        }
        else // Normal fall
        {
            _rb.gravityScale = _initialGravityScale;
        }
    }

    private void FixedUpdate()
    {
        _moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>();
        _moveInput.y = 0f;
        _rb.velocity = _moveInput * _speed;
        HandleGravity();
    }

    private void OnDrawGizmos()
    {
        if (_jumping)
            Gizmos.color = Color.red;
        else 
            Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_boxCenter, _boxSize);
    }
}