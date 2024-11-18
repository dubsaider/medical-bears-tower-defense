public abstract class AoEShooter
{
    public float Range { get; protected set; }
    public float Damage { get; protected set; }

    public abstract void ShootAoE();

    protected void CommonAoEShootLogic()
    {
        // Общая логика выстрела по области
    }
}