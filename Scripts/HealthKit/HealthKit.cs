using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    [SerializeField] private float _healingAmount = 15f;
    
    public float HealingAmount { get; private set; }

    private void Start()
    {
        HealingAmount = _healingAmount;
    }

    public void PickUp()
    {
        Destroy(gameObject);
    }
}
