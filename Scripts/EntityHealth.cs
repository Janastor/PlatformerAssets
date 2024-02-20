using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : MonoBehaviour
{
    public event UnityAction OutOfHealth;
    public event UnityAction HealthChanged;

    public float Health { get; private set; }

    public void SetHealth(float health)
    {
        Health = health;
    }

    public void DecreaseHealth(float amount)
    {
        Health -= amount;

        if (Health <= 0)
        {
            OutOfHealth?.Invoke();
            Health = 0;
        }
        
        HealthChanged?.Invoke();
    }

    public void AddHealth(float amount)
    {
        Health += amount;
        HealthChanged?.Invoke();
    }
}