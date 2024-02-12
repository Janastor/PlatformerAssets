using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private Collider2D _hurtBox;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackCooldown = 0.8f;
    [SerializeField] private KeyCode _attackKey;

    private Player _player;
    private bool _canAttack = true;
    private WaitForSeconds _attackCooldownWait;
    private List<Collider2D> _contacts = new List<Collider2D>();
    private ContactFilter2D _contactFilter = new ContactFilter2D();
    
    public event UnityAction Attacked;
    
    private void Start()
    {
        _player = GetComponent<Player>();
        _attackCooldownWait = new WaitForSeconds(_attackCooldown);
        _contactFilter.useTriggers = true;
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
        Attacked?.Invoke();
        
        _hurtBox.OverlapCollider(_contactFilter, _contacts);
        print(_contacts.Count);

        foreach (Collider2D collision in _contacts)
        {
            if (collision.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage(_damage);
        }
        
        ActivateAttackCooldown();
    }

    private void ActivateAttackCooldown()
    {
        _canAttack = false;
        StartCoroutine(CooldownCoroutine());
        print("OnCooldown");
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return _attackCooldownWait;
        
        _canAttack = true;
    }
}
