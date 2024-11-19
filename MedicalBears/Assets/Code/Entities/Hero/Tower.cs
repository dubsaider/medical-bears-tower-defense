using UnityEngine;

public class Tower : Hero, SingleShooter, AoEShooter, MeleeAttacker
{
    public GameObject arrowPrefab; 
    public Transform firePoint; 
    private float nextTimeToFire = 0f;
    private LayerMask enemyLayerMask;
    public bool IsBuilded { get; private set; } 

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        enemyLayerMask = LayerMask.GetMask("Enemy");
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.green;
    }

    void Update()
    {
        if (IsBuilded && Time.time >= nextTimeToFire) 
        {
            Attack();
            nextTimeToFire = Time.time + 1f / attackSpeed;
        }
    }

    public override void ShootSingle(Transform target)
    {
        CommonShootLogic(target);
        // Дополнительная логика одиночного выстрела
    }

    public override void ShootAoE()
    {
        CommonAoEShootLogic();
        // Дополнительная логика выстрела по области
    }

    public override void Attack()
    {
        CommonAttackLogic();
        // Дополнительная логика ближнего боя
    }

    public void ToggleTower(bool isEnabled)
    {
        // Логика включения/выключения башни
    }

    public void HandleCorruption(Corruption corruption)
    {
        // Логика обработки заражения
    }

    private void Shoot(Vector3 targetPosition, Transform targetTransform)
    {
        Vector3 direction = (targetPosition - firePoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject bullet = Instantiate(arrowPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));
        Rigidbody2D rb = bullet.AddComponent<Rigidbody2D>();
        rb.velocity = direction * 10f;
        CircleCollider2D collider = bullet.AddComponent<CircleCollider2D>();
        collider.isTrigger = true; 

        Bullet bulletScript = bullet.AddComponent<Bullet>();
        bulletScript.SetTarget(targetTransform); 
        bulletScript.SetDamage(damage);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void SetBuildStatus(bool buildStatus)
    {
        IsBuilded = buildStatus;

        if (IsBuilded)
        {
            gameObject.tag = "Tower";
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.white;
            }
        }
        else
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.green;
            }
        }
    }
}