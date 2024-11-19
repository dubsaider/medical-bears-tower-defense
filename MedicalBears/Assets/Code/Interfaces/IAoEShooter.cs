using UnityEngine;

public interface IAoEShooter
{
    float Range { get; }
    float Damage { get; }

    void ShootAoE();
}