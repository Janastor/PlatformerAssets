using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Animator))]

public class EnemyAnimator : MonoBehaviour
{
    private const string AnimationTookDamage = "tookDamage";
    private const string AnimationDeath = "death";
    
    private Enemy _enemy;
    private Animator _animator;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
        
        _enemy.TookDamage += PlayDamageAnimation;
        _enemy.Died += PlayDeathAnimation;
    }

    private void OnDestroy()
    {
        _enemy.TookDamage -= PlayDamageAnimation;
        _enemy.Died -= PlayDeathAnimation;
    }

    private void PlayDamageAnimation()
    {
        _animator.SetTrigger(AnimationTookDamage);
    }

    private void PlayDeathAnimation()
    {
        _animator.SetTrigger(AnimationDeath);
    }
}
