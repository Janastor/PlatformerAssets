using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private KeyCode _moveLeft;
    [SerializeField] private KeyCode _moveRight;
    [SerializeField] private KeyCode _jump;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _movePower;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _drag;
    [SerializeField] private ContactFilter2D _jumpableSurface;

    private float _distanceToJump = 0.1f;
    private float _horizontal;
    private readonly RaycastHit2D[] _raycastHits = new RaycastHit2D[1];
    private Animator _animator;
    private bool _isAlive = true;

    private Rigidbody2D _rigidbody;

    public void Die()
    {
        if (_isAlive == false)
            return;
        
        _animator.SetTrigger("death");
        StartCoroutine(Death());
        enabled = false;
        _isAlive = false;
    }
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_jump))
            Jump();

        if (Input.GetKey(_moveLeft))
            MoveLeft();
        
        if (Input.GetKey(_moveRight))
            MoveRight();

        if (Input.GetKey(_moveRight) == false && Input.GetKey(_moveLeft) == false && _rigidbody.velocity.x != 0f)
            SlowDown();

        if (_rigidbody.velocity.x != 0)
            _animator.SetTrigger("isRunning");
        else
            _animator.ResetTrigger("isRunning");
        
        if (IsGrounded())
            _animator.SetTrigger("isGrounded");
        else
            _animator.ResetTrigger("isGrounded");
    }

    private void SlowDown()
    {
        Vector2 velocity = Vector2.MoveTowards(_rigidbody.velocity, new Vector2(0, _rigidbody.velocity.y), _drag);
        _rigidbody.velocity = velocity;
    }

    private void MoveLeft()
    {
        if (_rigidbody.velocity.x >= -_maxVelocity)
            _rigidbody.velocity += Vector2.left * _movePower * Time.deltaTime;

        Vector3 scale = transform.localScale;
        scale.x = -1f;
        transform.localScale = scale;
    }

    private void MoveRight()
    {
        if (_rigidbody.velocity.x <= _maxVelocity)
            _rigidbody.velocity += Vector2.right * _movePower * Time.deltaTime;

        Vector3 scale = transform.localScale;
        scale.x = 1f;
        transform.localScale = scale;
    }

    private bool IsGrounded()
    {
        int contactCount = _rigidbody.Cast(Vector2.down, _jumpableSurface, _raycastHits, _distanceToJump);
        return (contactCount > 0);
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpPower);
            _animator.SetTrigger("jump");
        }
    }

    private IEnumerator Death()
    {
        while (_rigidbody.velocity.x != 0)
        {
            if (IsGrounded())
            {
                Vector2 velocity =
                    Vector2.MoveTowards(_rigidbody.velocity, new Vector2(0, _rigidbody.velocity.y), _drag);
                _rigidbody.velocity = velocity;
            }
            yield return null;
        }
    }
}
