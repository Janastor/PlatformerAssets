using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    private CoinSpawnpoint _spawnpoint;
    public event UnityAction PickedUp;
    
    public void Init(CoinSpawnpoint spawnpoint)
    {
        _spawnpoint = spawnpoint;
    }
    
    public void PickUp()
    {
        Destroy(gameObject);
        PickedUp?.Invoke();
    }
}
