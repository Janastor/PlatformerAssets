using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _damage;

    private float _deathDuration = 1f;
    
    public event UnityAction TookDamage;
    public event UnityAction Died;
    
    public float CurrentHealth { get; private set; }
    public bool IsAlive { get; private set; }
    
    public float Damage => _damage;

    private void Start()
    {
        CurrentHealth = _health;
        IsAlive = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player) && IsAlive)
            player.TryTakeDamage(_damage);
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
