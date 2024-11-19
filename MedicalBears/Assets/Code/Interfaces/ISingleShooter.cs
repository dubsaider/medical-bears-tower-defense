using UnityEngine;

public interface ISingleShooter
{
    float Range { get; }
    float Damage { get; }

    void ShootSingle();
}