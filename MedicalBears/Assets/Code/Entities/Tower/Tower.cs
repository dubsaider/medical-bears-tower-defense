using UnityEngine;
using Code.Enums;

public class Tower : Hero
{
    public TowerType TowerType;
    public Transform FirePoint;
    public ITowerAttackComponent AttackComponent { get; private set; }
    public bool IsBuilded { get; private set; }
    private Animator _animator;


    private float nextTimeToAttack = 0f;
    private SpriteRenderer spriteRenderer;

    public bool IsMaxLevel => Level == MaxLevel;
    public int Level { get; private set; } = 1;
    [SerializeField] private int MaxLevel = 3;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.green;

        _animator = GetComponent<Animator>();

        AttackComponent = GetComponent<ITowerAttackComponent>();
        if (AttackComponent == null)
        {
            // Debug.LogError($"No attack component found implementing ITowerAttackComponent on Tower: {TowerType}", gameObject);
        }
    }

    private void Update()
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

    /// <summary>
    /// Метод для повышения уровня башни
    /// </summary>
    public void UpgradeLevel()
    {
        if (IsMaxLevel) 
            return;
        
        Level++;
            // Увеличиваем характеристики на 20%
        attackRange *= 1.2f;
        damage *= 1.2f;
        attackSpeed *= 1.2f;
        UpdateView();
    }

    /// <summary>
    /// Метод обновления визуала башни (после улучшения)
    /// </summary>
    private void UpdateView()
    {
        //TODO сделать смену спрайта
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public override void Die()
    {
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
