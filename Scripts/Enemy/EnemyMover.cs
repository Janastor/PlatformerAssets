using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _pointsContainer;
    
    private Transform[] _points;
    private int _targetPointIndex = 0;
    
    private Vector3 _targetPosition => _points[_targetPointIndex].position;
    
    private void Start()
    {
        _points = new Transform[_pointsContainer.childCount];
        
        for (int i = 0; i < _points.Length; i++)
            _points[i] = _pointsContainer.GetChild(i);

        transform.position = _points[0].position;
    }
    
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);

        if (transform.position == _targetPosition)
        {
            _targetPointIndex++;
            _targetPointIndex %= _points.Length;
        }
    }
}
