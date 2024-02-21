using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _attackCooldown;
    
    private bool _canAttack = true;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.Died += OnEnemyDied;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player) && _canAttack)
            Attack(player);
    }

    private void Attack(Player player)
    {
        player.TryTakeDamage(_damage);
        _canAttack = false;
        StartCoroutine(AttackCooldownCoroutine());
    }
    
    private void OnEnemyDied()
    {
        _canAttack = false;
    }

    private IEnumerator AttackCooldownCoroutine()
    {
        yield return new WaitForSeconds(_attackCooldown);

        _canAttack = true;
    }
}
