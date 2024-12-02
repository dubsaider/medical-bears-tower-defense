using UnityEngine;

public class XRayAttackComponent : MonoBehaviour, ITowerAttackComponent
{
    public GameObject wavePrefab; // Префаб волны
    [SerializeField] private float waveDamage = 10f; // Урон волны
    [SerializeField] private float range = 5f; // Радиус действия волны
    [SerializeField] private float angle = 60f; // Угол сектора действия волны
    [SerializeField] private float pullForce = 1f; // Угол сектора действия волны


    public void Attack(Transform firePoint, float range, float damage, Transform target)
    {
        if (target != null)
        {
            Vector3 direction = (target.position - firePoint.position).normalized;  
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