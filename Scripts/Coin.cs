using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    [SerializeField] private UnityEvent _coinCollected;
    private CoinSpawnpoint _spawnpoint;
    
    public void Init(CoinSpawnpoint spawnpoint)
    {
        _spawnpoint = spawnpoint;
    }

    public event UnityAction CoinCollected
    {
        add => _coinCollected.AddListener(value);
        remove => _coinCollected.RemoveListener(value);
    }

    private void OnDestroy()
    {
        _spawnpoint.IsCoinSpawned = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player) == true)
        {
            _coinCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
