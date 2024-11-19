using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    private Hero hero;
    private LinkedList<Hero> attackQueue = new LinkedList<Hero>();

    void Start()
    {
        hero = GetComponentInParent<Hero>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Hero target))
        {
            if (target is FriendlyUnit)
            {
                attackQueue.AddLast(target);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Hero target))
        {
            attackQueue.Remove(target);
        }
    }

    void Update()
    {
        if (attackQueue.Count > 0)
        {
            if (hero is IMeleeAttacker meleeAttacker)
            {
                meleeAttacker.Attack();
            }
            else if (hero is ISingleShooter singleShooter)
            {
                singleShooter.ShootSingle();
            }
            else if (hero is IAoEShooter aoeShooter)
            {
                aoeShooter.ShootAoE();
            }
        }
    }
}