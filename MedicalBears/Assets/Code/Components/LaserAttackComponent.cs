using UnityEngine;

public class LaserAttackComponent : MonoBehaviour, ITowerAttackComponent
{
    [SerializeField] private LineRenderer lineRenderer;
    private float laserAttackRange;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer не найден на объекте " + gameObject.name);
        }
    }

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
            Vector2 direction = (nearestEnemy.position - firePoint.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, range, LayerMask.GetMask("Enemy"));

            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Enemy>()?.TakeDamage(damage);

                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, hit.point); 
            }
        }
        else
        {
            Vector3 laserEndPoint = firePoint.position + firePoint.right * range;
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, laserEndPoint);
        }
        laserAttackRange = range;
        lineRenderer.enabled = true;
    }

    private void Update()
    {
        if (!IsEnemyInRange())
        {
            lineRenderer.enabled = false;
        }
    }

    private bool IsEnemyInRange()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, laserAttackRange, LayerMask.GetMask("Enemy"));
        return enemies.Length > 0;
    }
}
