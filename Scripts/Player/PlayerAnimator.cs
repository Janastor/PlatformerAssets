using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerAttacker))]

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private Player _player;
    private PlayerMover _playerMover;
    private PlayerAttacker _playerAttacker;

    private const string AnimationDeath = "death";
    private const string AnimationIsRunning = "isRunning";
    private const string AnimationIsGrounded = "isGrounded";
    private const string AnimationJump = "jump";
    private const string AnimationAttack = "attack";
    private const string AnimationTookDamage = "tookDamage";

    private void Start()
    {
        _player = GetComponent<Player>();
        _playerMover = GetComponent<PlayerMover>();
        _playerAttacker = GetComponent<PlayerAttacker>();
        
        _playerMover.StartedRunning += PlayRunAnimation;
        _playerMover.StoppedRunning += StopRunAnimation;
        _playerMover.ChangedDirection += ChangeDirection;
        _playerMover.Jumped += PlayJumpAnimation;
        _playerMover.Landed += StopAirborneAnimation;
        _playerMover.Ungrounded += PlayAirborneAnimation;
        _player.Died += PlayDeathAnimation;
        _player.TookDamage += PlayDamageAnimation;
        _playerAttacker.Attacked += PlayAttackAnimation;
    }

    private void OnDestroy()
    {
        _playerMover.StartedRunning -= PlayRunAnimation;
        _playerMover.StoppedRunning -= StopRunAnimation;
        _playerMover.ChangedDirection -= ChangeDirection;
        _playerMover.Jumped -= PlayJumpAnimation;
        _playerMover.Landed -= StopAirborneAnimation;
        _playerMover.Ungrounded -= PlayAirborneAnimation;
        _player.Died -= PlayDeathAnimation;
        _player.TookDamage -= PlayDamageAnimation;
        _playerAttacker.Attacked -= PlayAttackAnimation;
    }

    private void ChangeDirection(float direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = direction;
        transform.localScale = scale;
    }

    private void PlayDamageAnimation()
    {
        _animator.SetTrigger(AnimationTookDamage);
    }

    private void PlayRunAnimation()
    {
        _animator.SetTrigger(AnimationIsRunning);
    }
    
    private void StopRunAnimation()
    {
        _animator.ResetTrigger(AnimationIsRunning);
    }

    private void PlayJumpAnimation()
    {
        _animator.SetTrigger(AnimationJump);
    }

    private void PlayAirborneAnimation()
    {
        _animator.ResetTrigger(AnimationIsGrounded);
    }
    
    private void StopAirborneAnimation()
    {
        _animator.SetTrigger(AnimationIsGrounded);
    }

    private void PlayDeathAnimation()
    {
        _animator.SetTrigger(AnimationDeath);
    }

    private void PlayAttackAnimation()
    {
        _animator.SetTrigger(AnimationAttack);
    }
}
