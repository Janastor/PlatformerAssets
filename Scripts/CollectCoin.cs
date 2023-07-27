using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class CollectCoin : MonoBehaviour
{
    [SerializeField] private UnityEvent _coinCollected;

    public event UnityAction CoinCollected
    {
        add => _coinCollected.AddListener(value);
        remove => _coinCollected.RemoveListener(value);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player) == true)
        {
            _coinCollected.Invoke();
            Destroy(gameObject);
        }
    }
}
