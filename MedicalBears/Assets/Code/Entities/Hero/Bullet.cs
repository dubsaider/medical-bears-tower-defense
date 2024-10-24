using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f; 
    public float speed = 10f;
    private float damage; // Поле для хранения урона

    private Transform target;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); 
            }
            Destroy(gameObject);
        }
    }
}