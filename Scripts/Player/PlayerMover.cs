using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Player))]

public class PlayerMover : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private KeyCode _moveLeft;
    [SerializeField] private KeyCode _moveRight;
    [SerializeField] private KeyCode _jump;
    [Header("Movement")]
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _movePower;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _dragAmount;
    [SerializeField] private ContactFilter2D _jumpableSurface;
    
    private readonly RaycastHit2D[] _raycastHits = new RaycastHit2D[1];
    private Rigidbody2D _rigidbody;
    private Player _player;
    private float _groundGapToJump = 0.1f;
    private float _rbDragAfterDeath = 1;
    private Transform _spriteTransform;
    private float _moveDirectionRight = 1;
    private float _moveDirectionLeft = -1;
    private bool _wasGrounded;
    private bool _isGrounded;
    private bool _wasRunning;
    private bool _isRunning;
    
    public event UnityAction StartedRunning;
    public event UnityAction StoppedRunning;
    public event UnityAction<float> ChangedDirection;
    public event UnityAction Jumped;
    public event UnityAction Landed;
    public event UnityAction Ungrounded;

    private void Die()
    {
        StartCoroutine(DeathSequence());
        enabled = false;
    }
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
        _player.Died += Die;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_jump))
            Jump();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(_moveLeft))
            Move(_moveDirectionLeft);
        
        if (Input.GetKey(_moveRight))
            Move(_moveDirectionRight);

        if (Input.GetKey(_moveRight) == false && Input.GetKey(_moveLeft) == false && _rigidbody.velocity.x != 0f)
            SlowDown();
        
        if (Input.GetKey(_moveRight) && Input.GetKey(_moveLeft))
            SlowDown();

        LandedEventCheck();
        RunningEventCheck();
    }

    private void LandedEventCheck()
    {
        _isGrounded = IsGrounded();
        
        if (_isGrounded && _wasGrounded == false)
            Landed?.Invoke();
        
        if (_isGrounded == false && _wasGrounded)
            Ungrounded?.Invoke();
        
        _wasGrounded = _isGrounded;
    }

    private void RunningEventCheck()
    {
        _isRunning = _rigidbody.velocity.x != 0;
        
        if (_isRunning && _wasRunning == false)
            StartedRunning?.Invoke();
        
        
        if (_isRunning == false && _wasRunning == true)
            StoppedRunning?.Invoke();
        
        _wasRunning = _isRunning;
    }

    private void SlowDown()
    {
        Vector2 velocity = Vector2.MoveTowards(_rigidbody.velocity, new Vector2(0, _rigidbody.velocity.y), _dragAmount);
        _rigidbody.velocity = velocity;
    }

    private void Move(float moveDirection)
    {
        if (Mathf.Abs(_rigidbody.velocity.x) <= _maxVelocity)
            _rigidbody.AddForce(Vector2.right * moveDirection * _movePower, ForceMode2D.Force);

        ChangedDirection?.Invoke(moveDirection);
    }

    private bool IsGrounded()
    {
        int contactCount = _rigidbody.Cast(Vector2.down, _jumpableSurface, _raycastHits, _groundGapToJump);
        
        return (contactCount > 0);
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            _rigidbody.velocity += Vector2.up * _jumpPower;
            Jumped?.Invoke();
        }
    }

    private IEnumerator DeathSequence()
    {
        while (_rigidbody.velocity.x != 0)
        {
            if (IsGrounded())
            {
                SlowDown();
            }

            _rigidbody.drag = _rbDragAfterDeath;
            
            yield return null;
        }
    }
}
