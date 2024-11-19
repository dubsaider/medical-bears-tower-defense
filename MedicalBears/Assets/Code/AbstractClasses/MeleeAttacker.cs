using UnityEngine;

public abstract class MeleeAttacker
{
    public float attackRange;
    public float damage;

    public virtual void Attack(Transform target)
    {
        if (target == null)
        {
            Debug.LogWarning("Target is null.");
            return;
        }

        if (Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            // Применение урона цели
            ApplyDamage(target);
        }
    }

    protected virtual void ApplyDamage(Transform target)
    {
        // Логика применения урона
        Debug.Log($"Dealing {damage} damage to {target.name}");
    }
}