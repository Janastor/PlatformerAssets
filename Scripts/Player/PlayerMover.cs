using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Player))]

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [Header("Controls")]
    [SerializeField] private KeyCode _moveLeft;
    [SerializeField] private KeyCode _moveRight;
    [SerializeField] private KeyCode _jump;
    [Header("Movement")]
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _movePower;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _drag;
    [SerializeField] private ContactFilter2D _jumpableSurface;

    private const string AnimationDeath = "death";
    private const string AnimationIsRunning = "isRunning";
    private const string AnimationIsGrounded = "isGrounded";
    private const string AnimationJump = "jump";

    private readonly RaycastHit2D[] _raycastHits = new RaycastHit2D[1];
    private float _groundGapToJump = 0.1f;
    private float _horizontal;
    private bool _isAlive = true;
    private Rigidbody2D _rigidbody;
    private Transform _spriteTransform;
    private float _moveDirectionRight = 1;
    private float _moveDirectionLeft = -1;

    public void Die()
    {
        if (_isAlive == false)
            return;
        
        _animator.SetTrigger(AnimationDeath);
        StartCoroutine(DeathSequence());
        enabled = false;
        _isAlive = false;
    }
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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

        if (_rigidbody.velocity.x != 0)
            _animator.SetTrigger(AnimationIsRunning);
        else
            _animator.ResetTrigger(AnimationIsRunning);
        
        if (IsGrounded())
            _animator.SetTrigger(AnimationIsGrounded);
        else
            _animator.ResetTrigger(AnimationIsGrounded);
    }

    private void SlowDown()
    {
        Vector2 velocity = Vector2.MoveTowards(_rigidbody.velocity, new Vector2(0, _rigidbody.velocity.y), _drag);
        _rigidbody.velocity = velocity;
    }

    private void Move(float moveDirection)
    {
        if (Mathf.Abs(_rigidbody.velocity.x) <= _maxVelocity)
            _rigidbody.AddForce(Vector2.right * moveDirection * _movePower, ForceMode2D.Force);

        Vector3 scale = transform.localScale;
        scale.x = moveDirection;
        _animator.transform.localScale = scale;
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
            _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            _animator.SetTrigger(AnimationJump);
        }
    }

    private IEnumerator DeathSequence()
    {
        while (_rigidbody.velocity.x != 0)
        {
            if (IsGrounded())
            {
                Vector2 velocity = Vector2.MoveTowards(_rigidbody.velocity, new Vector2(0, _rigidbody.velocity.y), _drag);
                _rigidbody.velocity = velocity;
            }
            
            yield return null;
        }
    }
}
