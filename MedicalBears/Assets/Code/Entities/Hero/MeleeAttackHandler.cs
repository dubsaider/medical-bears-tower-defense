using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackHandler : MonoBehaviour
{
    private Enemy enemy;
    private LinkedList<Hero> attackQueue = new LinkedList<Hero>();

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Hero hero))
        {
            if (hero is FriendlyUnit)
            {
                attackQueue.AddLast(hero);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Hero hero))
        {
            attackQueue.Remove(hero);
        }
    }

    void Update()
    {
        if (attackQueue.Count > 0)
        {
            Hero target = attackQueue.First.Value;
            if (target.IsAlive())
            {
                target.TakeDamage(enemy.GetDamage());
            }
            else
            {
                attackQueue.RemoveFirst();
            }
        }
    }
}