using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerHealth))]

public class Player : MonoBehaviour
{
    [SerializeField] private CoinCounter _coinCounter;
    [SerializeField] private float _health;
    [SerializeField] private float _damageImmunityDuration = 1f;

    private bool _isAlive = true;
    private bool _canTakeDamage = true;
    private PlayerHealth _playerHealth;

    public event UnityAction Died;
    public event UnityAction TookDamage;
    public event UnityAction Healed;

    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _playerHealth.SetHealth(_health);
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

        if (collision.TryGetComponent(out PlayerKiller _))
            Die();
    }

    public void TryTakeDamage(float damage)
    {
        if (_canTakeDamage == false || _isAlive == false)
            return;
        
        _playerHealth.DecreaseHealth(damage);
        ActivateIFrame();
        TookDamage?.Invoke();
    }

    private void Die()
    {
        if (_isAlive == false)
            return;
        
        Died?.Invoke();
        _isAlive = false;
        StopCoroutine(DamageImmunityCoroutine());
    }

    private void ActivateIFrame()
    {
        _canTakeDamage = false;
        StartCoroutine(DamageImmunityCoroutine());
    }

    private IEnumerator DamageImmunityCoroutine()
    {
        yield return new WaitForSeconds(_damageImmunityDuration);

        _canTakeDamage = true;
    }
}
