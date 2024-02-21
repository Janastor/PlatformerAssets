using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFollower : MonoBehaviour
{
    [SerializeField] private EntityHealth _entity;
    [SerializeField] private Vector3 _offset;
    
    private Transform target;
    
    private void Start()
    {
        target = _entity.transform;
        _entity.OutOfHealth += OnEntityDied;
    } 
    
    private void Update()
    {
        transform.position = target.position + _offset;
    }
    
    private void OnDestroy()
    {
        _entity.OutOfHealth -= OnEntityDied;
    }

    private void OnEntityDied()
    {
        Destroy(gameObject);
    }
}
