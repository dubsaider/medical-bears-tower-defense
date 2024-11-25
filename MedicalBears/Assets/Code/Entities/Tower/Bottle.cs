using UnityEngine;

public class Bottle : MonoBehaviour
{
    [SerializeField] private float aoeRadius = 2f; 
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject explosionEffect; 

    private float damage; 
    private float explosionRange;

    public void Initialize(float damageValue, float range)
    {
        damage = damageValue;
        explosionRange = range > 0 ? range : aoeRadius; 
    }

    private void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRange, enemyLayer);
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>()?.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Explode(); 
        }
    }

    private void OnDestroy()
    {
        // Дополнительная очистка при уничтожении объекта
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange > 0 ? explosionRange : aoeRadius);
    }
}
