using UnityEngine;

public class BottleAttackComponent : MonoBehaviour, ITowerAttackComponent
{
    [SerializeField] private GameObject bottlePrefab; 
    [SerializeField] private float throwForce = 8f;    

    public void Attack(Transform firePoint, float range, float damage)
    {
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
            Vector3 targetPosition = nearestEnemy.position;
            GameObject bottle = Instantiate(bottlePrefab, firePoint.position, Quaternion.identity);

            Bottle bottleScript = bottle.GetComponent<Bottle>();
            if (bottleScript != null)
            {
                bottleScript.Initialize(damage, range);
                bottleScript.SetTargetPosition(targetPosition);
            }

            Rigidbody2D rb = bottle.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector3 direction = (targetPosition - firePoint.position).normalized;
                rb.velocity = direction * throwForce; 
                rb.gravityScale = 0;  
            }
        }
    }
}