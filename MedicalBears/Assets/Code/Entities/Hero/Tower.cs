using UnityEngine;

public class Tower : Hero, ISingleShooter, IAoEShooter, IMeleeAttacker
{
    public GameObject arrowPrefab; 
    public Transform firePoint; 
    private float nextTimeToFire = 0f;
    private LayerMask enemyLayerMask;
    public bool IsBuilded { get; private set; } 

    private SpriteRenderer spriteRenderer;

    public float Range { get; private set; }
    public float Damage { get; private set; }

    void Awake()
    {
        enemyLayerMask = LayerMask.GetMask("Enemy");
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.green;

        Range = attackRange;
        Damage = damage;
    }

    void Update()
    {
        if (IsBuilded && Time.time >= nextTimeToFire) 
        {
            Attack();
            nextTimeToFire = Time.time + 1f / attackSpeed;
        }
    }

    public void ShootSingle(Transform target)
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

    public void ShootAoE()
    {
        // Логика выстрела по области
        Debug.Log("Performing AoE attack.");
    }

    public void Attack(Transform target)
    {
        if (target == null)
        {
            Debug.LogWarning("Target is null.");
            return;
        }

        if (Vector3.Distance(transform.position, target.position) <= Range)
        {
            // Логика ближнего боя
            Debug.Log($"Dealing {Damage} damage to {target.name}");
        }
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
        bulletScript.SetDamage(Damage);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
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

    public override int GetAttackPriority(Hero target)
    {
        return 0;
    }

    public override void Die()
    {
        Destroy(gameObject);
    }

    public override void Move()
    {
        throw new System.NotImplementedException("Tower cannot move.");
    }
}