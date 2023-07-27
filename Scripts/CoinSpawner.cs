using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private CoinCounter _counter;
    private static Random _random = new Random();
    private CoinSpawnpoint[] _spawnPoints = { };
    private int _minRandom = 0;
    private int _maxRandom;

    private void Start()
    {
        _spawnPoints = GetComponentsInChildren<CoinSpawnpoint>();
        _maxRandom = _spawnPoints.Length;
        SpawnInRandomSpawner();
    }

    private void SpawnInRandomSpawner()
    {
        CoinSpawnpoint randomSpawnPoint = _spawnPoints[_random.Next(_minRandom, _maxRandom)];

        if (randomSpawnPoint.IsCoinSpawned == false)
        {
            Coin coin = Instantiate(_coin, randomSpawnPoint.transform.position, Quaternion.identity);
            coin.Init(randomSpawnPoint);
            randomSpawnPoint.IsCoinSpawned = true;
            coin.CoinCollected += _counter.AddCoin;
        }

        StartCoroutine(SpawnTimer());
    }

    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(_spawnDelay);
        SpawnInRandomSpawner();
    }
}