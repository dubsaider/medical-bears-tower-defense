using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackSpeed;

    protected bool isAlive = true;
    protected bool isAttacking = false;

    public abstract void Attack();
    public abstract void Move();
    public abstract void Die();

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && isAlive)
        {
            Die();
            isAlive = false;
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

    public float GetSpeed()
    {
        return speed;
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetAttackRange()
    {
        return attackRange;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

    public abstract int GetAttackPriority(Hero target);
}