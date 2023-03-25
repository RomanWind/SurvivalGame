using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SimpleFlash _flashEffect;
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _health = 0;
    private Vector3 _moveDelta;
    private Animator _animator;
    private float _movementSpeed = 7f;
    private float _attackCooldown = 1.5f;
    private float _lastAttack;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _health = _maxHealth;
    }

    public void MovePlayer()
    {
        _moveDelta = new Vector3(1, 0, 0);
        transform.Translate(_moveDelta.x * Time.fixedDeltaTime * _movementSpeed, 0, 0);
        _animator.SetFloat("Speed", 1f);
    }

    public void StopPlayerRunAnimation()
    {
        _animator.SetFloat("Speed", -1f);
    }

    public bool AttackEnemy()
    {
        if (Time.time - _lastAttack > _attackCooldown)
        {
            _animator.SetTrigger("PlayerAttacking");
            _lastAttack = Time.time;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RecieveDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            //game over
        }
        _flashEffect.Flash();
    }

    public float GetPlayerDamage()
    {
        return _damage;
    }

    public float GetPlayerHealth()
    {
        return _health;
    }

    public float GetPlayerMaxHealth()
    {
        return _maxHealth;
    }
}
