using UnityEngine;

public class ArrowAttackComponent : ITowerAttackComponent
{
    private GameObject arrowPrefab;

    public ArrowAttackComponent(GameObject arrowPrefab)
    {
        this.arrowPrefab = arrowPrefab;
    }

    public void Attack(Transform firePoint, float range, float damage)
    {
        // Одиночный выстрел
        Collider2D[] enemies = Physics2D.OverlapCircleAll(firePoint.position, range, LayerMask.GetMask("Enemy"));
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(firePoint.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        if (nearestEnemy != null)
        {
            Vector3 direction = (nearestEnemy.position - firePoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GameObject arrow = Object.Instantiate(arrowPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));
            Rigidbody2D rb = arrow.AddComponent<Rigidbody2D>();
            rb.velocity = direction * 10f;
            Debug.Log($"Arrow shoots at {nearestEnemy.name}.");
        }
    }
}