using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SimpleFlash _flashEffect;
    [SerializeField] private float _maxHealth = 10;
    [SerializeField] private float _attack = 1;
    [SerializeField] private float _health = 0;
    private Animator _animator;
    private float _attackCooldown = 2f;
    private float _lastAttack;
    private bool _isEnemyAlive = true;

    private void Start()
    {
        _health = _maxHealth;
        _animator = gameObject.GetComponent<Animator>();
    }

    public bool DealDamage()
    {
        if (Time.time - _lastAttack > _attackCooldown && _isEnemyAlive)
        {
            _animator.SetTrigger("Attack");
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
        if(_health <= 0)
        {
            Death();
            return;
        }
        _flashEffect.Flash();
    }

    public void Death()
    {
        _isEnemyAlive = false;
        _animator.SetTrigger("EnemyIsDead");
        //give rewards
        //GiveRewards()
    }

    public float GetEnemyHealth()
    {
        return _health;
    }

    public float GetEnemyAttack()
    {
        return _attack;
    }
}
