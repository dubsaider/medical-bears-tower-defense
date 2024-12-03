using System.Collections;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected ParticleSystem ParticleTakeDamage;
    [SerializeField] protected ParticleSystem ParticleDeath;


    protected bool isAlive = true;
    protected bool isAttacking = false;

    public int pursuitCnt = 0;

    public abstract void Move();
    public abstract void Die();
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (ParticleTakeDamage != null) { ParticleTakeDamage.Play(); }
        if (health <= 0 && isAlive)
        {
            isAlive = false;
            Die();
        }
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(float value)
    {
        health = value;
    }
    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }
    public float GetDamage()
    {
        return damage;
    }

    public void SetDamage(float value)
    {
        damage = value;
    }
    public float GetAttackRange()
    {
        return attackRange;
    }

    public void SetAttackRange(float value)
    {
        attackRange = value;
    }
    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

    public void SetAttackSpeed(float value)
    {
        attackSpeed = value;
    }
    public abstract int GetAttackPriority(Hero target);
}