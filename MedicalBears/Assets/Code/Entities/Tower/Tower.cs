using UnityEngine;
using Code.Enums;

public class Tower : Hero
{
    public TowerType TowerType;
    public Transform FirePoint;
    public ITowerAttackComponent AttackComponent { get; private set; }
    public bool IsBuilded { get; private set; }

    private float nextTimeToAttack = 0f;
    private SpriteRenderer spriteRenderer;

    public int Level { get; private set; } = 1; 
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.green;

        AttackComponent = GetComponent<ITowerAttackComponent>();
        if (AttackComponent == null)
        {
            Debug.LogError($"No attack component found implementing ITowerAttackComponent on Tower: {TowerType}", gameObject);
        }
    }

    void Update()
    {
        if (IsBuilded && Time.time >= nextTimeToAttack)
        {
            AttackComponent?.Attack(FirePoint, attackRange, damage);
            nextTimeToAttack = Time.time + 1f / attackSpeed;
        }
    }

    public void SetBuildStatus(bool buildStatus)
    {
        IsBuilded = buildStatus;
        spriteRenderer.color = buildStatus ? Color.white : Color.green;
        if (IsBuilded)
        {
            gameObject.tag = "Tower";
        }
    }

    // Метод для повышения уровня башни
    public void UpgradeLevel()
    {
        Level++; // Увеличиваем уровень на 1
        Debug.Log($"Tower {TowerType} upgraded to level {Level}");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public override void Die()
    {
        gameObject.SetActive(false);
    }

    public override void Move()
    {
        throw new System.NotImplementedException();
    }

    public override int GetAttackPriority(Hero target)
    {
        throw new System.NotImplementedException();
    }
}
