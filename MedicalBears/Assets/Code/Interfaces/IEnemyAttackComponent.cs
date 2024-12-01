using System.Collections;

public interface IEnemyAttackComponent
{
    float Range { get; }
    float Damage { get; }
    float AttackCooldown { get; }

    Hero Target { get; }
    bool HasTarget { get; }
    bool IsTargetInAttackRange { get; }

    IEnumerator FindAndFollowTarget();
    IEnumerator Attack();
}