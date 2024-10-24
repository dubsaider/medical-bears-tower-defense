using UnityEngine;

public class Tower : Hero
{
    public GameObject arrowPrefab; 
    public Transform firePoint; 
    private float nextTimeToFire = 0f;
    private LayerMask enemyLayerMask;

     void Start()
    {
        enemyLayerMask = LayerMask.GetMask("Enemy");
    }
    void Update()
    {
        if (Time.time >= nextTimeToFire)
        {
            Attack();
            nextTimeToFire = Time.time + 1f / attackSpeed;
        }
    }

    public override void Attack()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayerMask);
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
            Shoot(nearestEnemy.position, nearestEnemy);
        }
    }

    public override void Die()
    {
        Destroy(gameObject);
    }

    public override void Move()
    {
        throw new System.NotImplementedException("Tower cannot move.");
    }

    public override int GetAttackPriority(Hero target)
    {
        return 0;
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
}