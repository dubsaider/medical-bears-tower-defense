using UnityEngine;

public abstract class SingleShooter
{
    public float Range { get; protected set; }
    public float Damage { get; protected set; }

    public abstract void ShootSingle(Transform target);

    protected virtual void CommonShootLogic(Transform target)
    {
        if (target == null)
        {
            Debug.LogWarning("Target is null.");
            return;
        }

        if (Vector3.Distance(transform.position, target.position) <= Range)
        {
            // Логика одиночного выстрела
            Debug.Log($"Shooting at {target.name}");
        }
    }
}