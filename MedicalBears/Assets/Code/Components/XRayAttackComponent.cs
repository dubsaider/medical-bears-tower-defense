using UnityEngine;

public class XRayAttackComponent : MonoBehaviour, ITowerAttackComponent
{
    [SerializeField] private ParticleSystem pulseEffect; 
    [SerializeField] private float pulseRadius = 5f;
    [SerializeField] private float teleportDistance = 4f;
    [SerializeField] private Transform firePoint;

    
    public void Attack(Transform firePoint, float range, float damage)
    {
        if (pulseEffect != null)
        {
            pulseEffect.transform.position = firePoint.position;
            pulseEffect.Play();
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(firePoint.position, pulseRadius);
        foreach (var enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Vector3 directionToTower = (firePoint.position - enemy.transform.position).normalized;
                enemy.transform.position += directionToTower * teleportDistance;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, pulseRadius);
    }
}
