using UnityEngine;

public class Tower : Hero
{
    public GameObject arrowPrefab; 
    public Transform firePoint; 
    private float nextTimeToFire = 0f;

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
        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            Shoot(nearestEnemy.transform.position, nearestEnemy.transform);
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

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= attackRange)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
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