using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover))]

public class Player : MonoBehaviour
{
    [SerializeField] private CoinCounter _coinCounter;

    private bool _isAlive = true;

    public event UnityAction Died;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            _coinCounter.AddCoin();
            coin.PickUp();
        }

        if (collision.CompareTag("PlayerKiller"))
            Die();
    }

    private void Die()
    {
        if (_isAlive == false)
            return;
        
        Died?.Invoke();
        _isAlive = false;
    }
}
