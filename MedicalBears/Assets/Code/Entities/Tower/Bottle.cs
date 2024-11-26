using UnityEngine;
using System.Collections.Generic;

public class Bottle : MonoBehaviour
{
    [SerializeField] private float aoeRadius = 1.5f;  
    [SerializeField] private LayerMask enemyLayer;   
    [SerializeField] private GameObject explosionEffect; 

    private float damage;  
    private float explosionRange; 
    
    private HashSet<Enemy> hitEnemies = new HashSet<Enemy>();

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

        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, explosionRange, enemyLayer);

        foreach (var collider in enemiesInRange)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null && !hitEnemies.Contains(enemy))
            {
                enemy.TakeDamage(damage); 
                hitEnemies.Add(enemy); 
            }
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Explode(); 
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange > 0 ? explosionRange : aoeRadius);
    }
}
