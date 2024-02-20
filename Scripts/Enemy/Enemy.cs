using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyAttacker))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;

    private float _deathDuration = 1f;
    
    public event UnityAction TookDamage;
    public event UnityAction Died;
    
    public float CurrentHealth { get; private set; }
    public bool IsAlive { get; private set; }

    private void Start()
    {
        CurrentHealth = _health;
        IsAlive = true;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        TookDamage?.Invoke();

        if (CurrentHealth <= 0)
            Die();
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
