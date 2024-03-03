using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(EntityHealth))]

public class Player : MonoBehaviour
{
    [SerializeField] private CoinCounter _coinCounter;
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;

    private bool _isAlive = true;

    public event UnityAction Died;
    public event UnityAction TookDamage;
    public event UnityAction Healed;
    
    private EntityHealth _playerHealth;

    private void Awake()
    {
        _playerHealth = GetComponent<EntityHealth>();
        _playerHealth.SetHealth(_maxHealth, _health);
        _playerHealth.OutOfHealth += Die;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            _coinCounter.AddCoin();
            coin.PickUp();
        }
        
        if (collision.TryGetComponent(out HealthKit healthKit))
        {
            _playerHealth.AddHealth(healthKit.HealingAmount);
            Healed?.Invoke();
            healthKit.PickUp();
        }

        //if (collision.TryGetComponent(out PlayerKiller _))
        //    Die();
    }
    
    public void TakeFullHealthDamage()
    {
        TryTakeDamage(_maxHealth);
        TookDamage?.Invoke();
    }

    public void TryTakeDamage(float damage)
    {
        if (_isAlive == false)
            return;
        
        _playerHealth.DecreaseHealth(damage);
        TookDamage?.Invoke();
    }

    private void Die()
    {
        if (_isAlive == false)
            return;
        
        Died?.Invoke();
        _isAlive = false;
    }
}
