using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyAttacker))]
[RequireComponent(typeof(EntityHealth))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;

    private float _deathDuration = 1f;
    private EntityHealth _enemyHealth; 
    
    public event UnityAction TookDamage;
    public event UnityAction Died;
    
    public bool IsAlive { get; private set; }

    private void Start()
    {
        _enemyHealth = GetComponent<EntityHealth>();
        _enemyHealth.SetHealth(_health);
        _enemyHealth.OutOfHealth += Die;
        IsAlive = true;
    }

    public void TakeDamage(float damage)
    {
        _enemyHealth.DecreaseHealth(damage);
        TookDamage?.Invoke();
    }

    private void Die()
    {
        Died?.Invoke();
        IsAlive = false;
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(_deathDuration);
        
        Destroy(gameObject);
    }
}
