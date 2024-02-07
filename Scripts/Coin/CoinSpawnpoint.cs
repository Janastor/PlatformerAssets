using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnpoint : MonoBehaviour
{
    private bool _isCoinSpawned = false;
    private Coin _coinPrefab;
    
    public bool IsCoinSpawned => _isCoinSpawned;

    public void Init(Coin coinPrefab)
    {
        _coinPrefab = coinPrefab;
    }
    
    public void SpawnCoin()
    {
        Coin coin = Instantiate(_coinPrefab, transform.position, Quaternion.identity, transform);
        coin.Init(this);
        coin.PickedUp += OnCoinPickedUp;
        _isCoinSpawned = true;
    }

    private void OnCoinPickedUp()
    {
        _isCoinSpawned = false;
    }
}
