using UnityEngine;

public interface IMeleeAttacker
{
    float Range { get; }
    float Damage { get; }

    void Attack();
}