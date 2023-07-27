using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _secondPoint;
    [SerializeField] private float _moveTime;

    private Vector3 _moveFrom;
    private Vector3 _moveTo;
    private float _normalizedMoveDuration;
    private float _moveDuration;
    
    private void Start()
    {
        _moveFrom = _firstPoint.position;
        _moveTo = _secondPoint.position;
        _moveDuration = 0;
        _normalizedMoveDuration = 0;
        transform.position = _firstPoint.position;
    }
    
    private void Update()
    {
        transform.position = Vector3.MoveTowards(_moveFrom, _moveTo, _normalizedMoveDuration);
        _moveDuration += Time.deltaTime;
        _normalizedMoveDuration = _moveDuration / _moveTime;
        
        if (transform.position == _moveTo)
            ChangeDirection();
    }

    private void ChangeDirection()
    {
        Vector3 tempVector;
        _moveDuration = 0;
        _normalizedMoveDuration = 0;
        tempVector = _moveFrom;
        _moveFrom = _moveTo;
        _moveTo = tempVector;
    }
}
