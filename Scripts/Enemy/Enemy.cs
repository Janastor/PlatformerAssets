using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _damage;

    public void TakeDamage(float damage)
    {
        print($"Enemy {gameObject.name} took {damage} damage");
    }
}
