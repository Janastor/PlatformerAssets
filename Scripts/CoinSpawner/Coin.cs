using System;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public event UnityAction PickedUp;
    
    public void PickUp()
    {
        Destroy(gameObject);
        PickedUp?.Invoke();
    }
}
