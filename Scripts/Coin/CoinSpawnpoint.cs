using System;
using UnityEngine;

public class CoinSpawnpoint : MonoBehaviour
{
    private Coin _coinPrefab;
    public bool IsCoinSpawned { get; private set; }
    
    public void Init(Coin coinPrefab)
    {
        _coinPrefab = coinPrefab;
    }
    
    public void SpawnCoin()
    {
        Coin coin = Instantiate(_coinPrefab, transform.position, Quaternion.identity, transform);
        coin.PickedUp += OnCoinPickedUp;
        IsCoinSpawned = true;
    }

    private void OnDestroy()
    {
        _coinPrefab.PickedUp -= OnCoinPickedUp;
    }

    private void OnCoinPickedUp()
    {
        IsCoinSpawned = false;
    }
}
