using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _target;
    [SerializeField] private float _smoothing;
    [SerializeField] private float _VerticalOffset;

    private Vector3 _velocity;
    private Vector3 _targetPosition;
    private Vector3 _offset;
    private float _zAxisOffset = -10f;
    
    private void Start()
    {
        _offset = new(0f, _VerticalOffset, _zAxisOffset);
    }

    private void Update()
    {
        _targetPosition = _target.position;
        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition + _offset, ref _velocity, _smoothing);
    }
}
