public abstract class SingleShooter
{
    public float Range { get; protected set; }
    public float Damage { get; protected set; }

    public abstract void ShootSingle();

    protected void CommonShootLogic()
    {
        // Общая логика одиночного выстрела
    }
}