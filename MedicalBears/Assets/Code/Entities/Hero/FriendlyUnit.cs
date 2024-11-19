using UnityEngine;

public class FriendlyUnit : Hero, ICorruptionHealer
{
    public void HealCorruption(Corruption corruption)
    {
        // Логика лечения заражения
    }

    public override void Attack(Transform target)
    {
        // Логика атаки
    }

    public override void Die()
    {
        // Логика смерти
    }

    public override void Move()
    {
        // Логика движения
    }

    public override int GetAttackPriority(Hero target)
    {
        return 0;
    }
}