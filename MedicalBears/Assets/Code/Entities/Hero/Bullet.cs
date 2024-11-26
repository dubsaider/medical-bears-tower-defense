using Code.Entities.Map;
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
        if (other.TryGetComponent(out CellHandler cellHandler) )
        {
            MapCellType type = cellHandler.GetMapCellType();
            if (type == MapCellType.Border)
            {
                Destroy(gameObject);
            }
        }

        if (other.transform == target)
        {
            if (other.TryGetComponent(out Hero hero))
            {
                hero.TakeDamage(damage);
                Debug.Log($"Bullet hits {hero.name}, dealing {damage} damage");
            }
            Destroy(gameObject);
        }
    }
}