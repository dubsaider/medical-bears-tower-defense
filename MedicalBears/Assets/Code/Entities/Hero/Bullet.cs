using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private float damage;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == target)
        {
            if (other.TryGetComponent(out Hero hero))
            {
                hero.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}