using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _pointsContainer;
    [SerializeField] private float _chaseRange;
    [SerializeField] private float _chaseSpeedModifier;
    
    private Transform[] _points;
    private Transform _playerTransform;
    private int _targetPointIndex = 0;
    private bool _isChasingPlayer = false;
    
    private Vector3 _targetPointPosition => _isChasingPlayer ? _playerTransform.position : _points[_targetPointIndex].position;
    private Vector3 _playerPosition => _playerTransform.position;
    
    private void Start()
    {
        _points = new Transform[_pointsContainer.childCount];
        
        for (int i = 0; i < _points.Length; i++)
            _points[i] = _pointsContainer.GetChild(i);

        transform.position = _points[0].position;
    }
    
    private void Update()
    {
        if (ShouldChasePlayer())
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerPosition, _speed * _chaseSpeedModifier * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPointPosition, _speed * Time.deltaTime);

            if (transform.position == _targetPointPosition)
            {
                _targetPointIndex++;
                _targetPointIndex %= _points.Length;
            }
        }
    }

    private bool ShouldChasePlayer()
    {
        return true;
    }
}
