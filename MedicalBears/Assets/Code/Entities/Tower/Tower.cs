using UnityEngine;
using Code.Enums;
using System.Collections.Generic;

public class Tower : Hero
{
    public TowerType TowerType;
    public GameObject FirePoint;
    [SerializeField] private int RotationOffset;
    public ITowerAttackComponent AttackComponent { get; private set; }
    public bool IsBuilded { get; private set; }
    private float nextTimeToAttack = 0f;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer firePointRenderer;
    private Transform FirePointTransform;


    public bool IsMaxLevel => Level == MaxLevel;
    public int Level { get; private set; } = 1;
    [SerializeField] private int MaxLevel = 3;
    [SerializeField] private List<Sprite> levelSprites; // Список спрайтов для уровней

    private Transform currentTargetTransform;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.green;

        if (FirePoint != null)
        {
            FirePointTransform = FirePoint.transform;

            if (!FirePoint.TryGetComponent<SpriteRenderer>(out firePointRenderer))
            {
                Debug.LogWarning("FirePoint does not have a SpriteRenderer component.");
            }
        }
        else
        {
            Debug.LogError("FirePoint is not assigned in the inspector!");
        }

        AttackComponent = GetComponent<ITowerAttackComponent>();
        if (AttackComponent == null)
        {
            Debug.LogError($"No attack component found implementing ITowerAttackComponent on Tower: {TowerType}", gameObject);
        }

        UpdateView();
    }

    private void Update()
    {
        if (!IsBuilded) return;

        currentTargetTransform = FindNearestEnemy(FirePointTransform, attackRange)?.transform;
        isAttacking = currentTargetTransform != null;
        var isActiveTarget = FindNearestEnemy(FirePointTransform, attackRange)?.GetComponent<Hero>().IsAlive();

        if (isAttacking && isActiveTarget == true )
        {
            RotateFirePointTowardsTarget();

            if (Time.time >= nextTimeToAttack)
            {
                AttackComponent?.Attack(FirePointTransform, attackRange, damage, currentTargetTransform);
                nextTimeToAttack = Time.time + 1f / attackSpeed;
            }
        }
    }

    private void RotateFirePointTowardsTarget()
    {
        if (currentTargetTransform == null) return;

        Vector3 direction = currentTargetTransform.position - FirePointTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        FirePointTransform.rotation = Quaternion.Euler(0, 0, angle-RotationOffset);
    }
    public void SetBuildStatus(bool buildStatus)
    {
        IsBuilded = buildStatus;
        spriteRenderer.color = buildStatus ? Color.white : Color.green;

        if (IsBuilded)
        {
            gameObject.tag = "Tower";
        }

        if (firePointRenderer != null)
        {
            firePointRenderer.color = spriteRenderer.color;
        }
    }

    public void UpgradeLevel()
    {
        if (IsMaxLevel)
            return;

        Level++;
        attackRange *= 1.2f;
        damage *= 1.2f;
        attackSpeed *= 1.2f;
        UpdateView();
    }

    private void UpdateView()
    {
        if (levelSprites == null || levelSprites.Count == 0)
        {
            Debug.LogWarning("No sprites assigned for tower levels!");
            return;
        }

        if (Level - 1 < levelSprites.Count)
        {
            spriteRenderer.sprite = levelSprites[Level - 1];
        }
        else
        {
            Debug.LogWarning($"No sprite for level {Level}! Make sure the list has enough sprites.");
        }

        if (firePointRenderer != null)
        {
            firePointRenderer.color = spriteRenderer.color;
        }
    }


    public Collider2D FindNearestEnemy(Transform firePoint, float range)
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(firePoint.position, range, LayerMask.GetMask("Enemy"));
        Collider2D nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(firePoint.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public override void Die() { }

    public override void Move() { throw new System.NotImplementedException(); }

    public override int GetAttackPriority(Hero target) { throw new System.NotImplementedException(); }
}
