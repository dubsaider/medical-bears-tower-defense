using UnityEngine;

public class Tower : Hero, ISingleShooter, IAoEShooter
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
            ShootSingle();
            nextTimeToFire = Time.time + 1f / attackSpeed;
        }
    }

    public void ShootSingle()
    {
        // Логика одиночного выстрела
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, Range, enemyLayerMask);
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, hitCollider.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = hitCollider.transform;
            }
        }

        if (nearestEnemy != null)
        {
            // Логика выстрела
            Vector3 direction = (nearestEnemy.position - firePoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GameObject bullet = Instantiate(arrowPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));
            Rigidbody2D rb = bullet.AddComponent<Rigidbody2D>();
            rb.velocity = direction * 10f;
            CircleCollider2D collider = bullet.AddComponent<CircleCollider2D>();
            collider.isTrigger = true; 

            Bullet bulletScript = bullet.AddComponent<Bullet>();
            bulletScript.SetTarget(nearestEnemy); 
            bulletScript.SetDamage(Damage);

            Debug.Log($"Tower {gameObject.name} shoots single at {nearestEnemy.name}");
        }
    }

    public void ShootAoE()
    {
        // Логика выстрела по области
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, Range, enemyLayerMask);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Hero enemy))
            {
                enemy.TakeDamage(Damage);
                Debug.Log($"Tower {gameObject.name} performs AoE attack on {enemy.name}");
            }
        }
    }

    public void ToggleTower(bool isEnabled)
    {
        // Логика включения/выключения башни
        IsBuilded = isEnabled;
        spriteRenderer.color = isEnabled ? Color.white : Color.green;
        Debug.Log($"Tower {gameObject.name} is toggled {(isEnabled ? "on" : "off")}");
    }

    public void HandleCorruption(Corruption corruption)
    {
        // Логика обработки заражения
        // health -= corruption.Damage;
        Debug.Log($"Tower {gameObject.name} handles corruption, health reduced to {health}");
        if (health <= 0)
        {
            Die();
        }
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
        Debug.Log($"Tower {gameObject.name} build status set to {buildStatus}");
    }

    public override int GetAttackPriority(Hero target)
    {
        return 0;
    }

    public override void Die()
    {
        Debug.Log($"Tower {gameObject.name} dies");
        Destroy(gameObject);
    }

    public override void Move()
    {
        Debug.Log($"Tower {gameObject.name} cannot move");
        throw new System.NotImplementedException("Tower cannot move.");
    }
}