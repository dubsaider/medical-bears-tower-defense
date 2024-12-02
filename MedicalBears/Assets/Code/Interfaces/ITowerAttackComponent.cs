using UnityEngine;

public interface ITowerAttackComponent
{
    void Attack(Transform firePoint, float range, float damage);
}