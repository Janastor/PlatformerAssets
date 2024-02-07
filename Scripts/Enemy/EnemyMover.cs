using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _secondPoint;
    [SerializeField] private float _speed;

    private Vector3 _initialPosition;
    private Vector3 _targetPosition;

    private void Start()
    {
        _initialPosition = _firstPoint.position;
        _targetPosition = _secondPoint.position;
        transform.position = _initialPosition;
    }
    
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);

        if (transform.position == _targetPosition)
            (_initialPosition, _targetPosition) = (_targetPosition, _initialPosition);
    }
}
