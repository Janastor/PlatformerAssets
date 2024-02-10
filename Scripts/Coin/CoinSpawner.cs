using System.Collections;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private CoinCounter _counter;
    
    private CoinSpawnpoint[] _spawnPoints;
    private CoinSpawnpoint[] _availableSpawnPoints;
    private int _minRandom = 0;
    private int _maxRandom;
    private bool _isWorking = true;

    private void Start()
    {
        _spawnPoints = GetComponentsInChildren<CoinSpawnpoint>();

        foreach (CoinSpawnpoint spawnpoint in _spawnPoints)
        {
            spawnpoint.Init(_coinPrefab);
        }

        StartCoroutine(SpawningLoop());
    }

    private void TrySpawnInRandomSpawner()
    {
        _availableSpawnPoints = _spawnPoints.Where(s => s.IsCoinSpawned == false).ToArray();
        
        if (_availableSpawnPoints.Length == 0)
            return;
        
        _maxRandom = _availableSpawnPoints.Length;
        CoinSpawnpoint randomSpawnPoint = _availableSpawnPoints[Random.Range(_minRandom, _maxRandom)];
        randomSpawnPoint.SpawnCoin();
    }

    private IEnumerator SpawningLoop()
    {
        WaitForSeconds delay = new WaitForSeconds(_spawnDelay);
        
        while (_isWorking)
        {
            TrySpawnInRandomSpawner();
            
            yield return delay;
        }
    }
}