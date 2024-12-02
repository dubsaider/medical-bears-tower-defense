using UnityEngine;

public class XRayAttackComponent : MonoBehaviour, ITowerAttackComponent
{
    public GameObject wavePrefab; // Префаб волны
    [SerializeField] private float waveDamage = 10f; // Урон волны
    [SerializeField] private float range = 5f; // Радиус действия волны
    [SerializeField] private float angle = 60f; // Угол сектора действия волны
    [SerializeField] private float pullForce = 1f; // Угол сектора действия волны


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
            Vector3 direction = (nearestEnemy.position - firePoint.position).normalized;  
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  
            GameObject wave = Instantiate(wavePrefab, firePoint.position, Quaternion.Euler(0, 0, angle - 90));
            WaveProjectile waveComponent = wave.GetComponent<WaveProjectile>();
            if (waveComponent != null)
            {
                waveComponent.maxRadius = range;
                waveComponent.pullForce = pullForce;
                waveComponent.towerPosition = firePoint.position;
                waveComponent.damage = waveDamage;
            }
            Rigidbody2D rb = wave.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * 10f;
                rb.gravityScale = 0;
                rb.freezeRotation = true;
            }
            
        }
    }

}