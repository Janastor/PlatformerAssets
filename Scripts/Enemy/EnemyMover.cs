using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _pointsContainer;
    [SerializeField] private float _chaseRange = 2f;
    [SerializeField] private float _chaseSpeedModifier;
    [SerializeField] private Transform _player;
    [SerializeField] private Vector2 _playerPositionOffset;
    
    private Transform[] _points;
    private int _targetPointIndex = 0;
    private bool _isChasingPlayer = false;
    private Enemy _enemy;
    
    private Vector3 _targetPointPosition => _isChasingPlayer ? _player.position : _points[_targetPointIndex].position;
    private Vector3 _playerPosition => _player.position + (Vector3)_playerPositionOffset;
    
    private void Start()
    {
        _points = new Transform[_pointsContainer.childCount];
        _enemy = GetComponent<Enemy>();
        _enemy.Died += OnDeath;
        
        for (int i = 0; i < _points.Length; i++)
            _points[i] = _pointsContainer.GetChild(i);

        transform.position = _points[0].position;
    }
    
    private void Update()
    {
        Move();
        CheckPlayerProximity();
    }

    private void OnDestroy()
    {
        _enemy.Died -= OnDeath;
    }

    private void Move()
    {
        if (_isChasingPlayer)
            MoveTowardsPlayer();
        else
            MoveTowardsNextPoint();
    }
    
    private void MoveTowardsNextPoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPointPosition, _speed * Time.deltaTime);

        if (transform.position == _targetPointPosition)
        {
            _targetPointIndex++;
            _targetPointIndex %= _points.Length;
        }
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _playerPosition, _speed * _chaseSpeedModifier * Time.deltaTime);
    }

    private void CheckPlayerProximity()
    {
        if (Vector3.Distance(_playerPosition, transform.position) <= _chaseRange)
            _isChasingPlayer = true;
        else
            _isChasingPlayer = false;
    }

    private void OnDeath()
    {
        enabled = false;
    }
}
