using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMover))]

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private Collider2D _hurtBox;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackCooldown = 0.8f;
    [SerializeField] private KeyCode _attackKey;
    
    private PlayerMover _playerMover;
    private Collider2D _collider;
    private bool _canAttack = true;
    private WaitForSeconds _attackCooldownWait;
    private List<RaycastHit2D> _hits = new List<RaycastHit2D>();
    private ContactFilter2D _contactFilter = new ContactFilter2D();
    private Vector2 _attackDirection;

    public event UnityAction Attacked;
    
    private void Start()
    {
        _playerMover = GetComponent<PlayerMover>();
        _collider = GetComponent<Collider2D>();
        _attackCooldownWait = new WaitForSeconds(_attackCooldown);
        _contactFilter.useTriggers = true;
        _attackDirection = new Vector2(1, 0);
        _playerMover.ChangedDirection += ChangeAttackDirection;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_attackKey))
            TryAttack();
    }

    private void TryAttack()
    {
        if (_canAttack == false)
            return;

        Attack();
    }

    private void Attack()
    {
        _collider.Cast(_attackDirection, _contactFilter, _hits, _attackDistance);
        
        foreach (RaycastHit2D hit in _hits)
        {
            if (hit.collider.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage(_damage);
        }
        
        Attacked?.Invoke();
        ActivateAttackCooldown();
    }

    private void ChangeAttackDirection(float direction)
    {
        _attackDirection.x = direction;
    }

    private void ActivateAttackCooldown()
    {
        _canAttack = false;
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return _attackCooldownWait;
        
        _canAttack = true;
    }
}
