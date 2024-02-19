using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover))]

public class Player : MonoBehaviour
{
    [SerializeField] private CoinCounter _coinCounter;
    [SerializeField] private float _health;
    [SerializeField] private float _iFrameDuration = 1f;

    private bool _isAlive = true;
    private bool _canTakeDamage = true;

    public event UnityAction Died;
    public event UnityAction TookDamage;
    
    public float _currentHealth { get; private set; }
    
    private void Start()
    {
        _currentHealth = _health;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            _coinCounter.AddCoin();
            coin.PickUp();
        }

        if (collision.TryGetComponent(out PlayerKiller _))
            Die();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy) && enemy.IsAlive)
            TryTakeDamage(enemy.Damage);
    }

    private void TryTakeDamage(float damage)
    {
        if (_canTakeDamage == false || _isAlive == false)
            return;
        
        _currentHealth -= damage;
        ActivateIFrame();
        TookDamage?.Invoke();

        if (_currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        if (_isAlive == false)
            return;
        
        Died?.Invoke();
        _isAlive = false;
        StopCoroutine(IFrameCoroutine());
    }

    private void ActivateIFrame()
    {
        _canTakeDamage = false;
        StartCoroutine(IFrameCoroutine());
    }

    private IEnumerator IFrameCoroutine()
    {
        yield return new WaitForSeconds(_iFrameDuration);

        _canTakeDamage = true;
    }
}
