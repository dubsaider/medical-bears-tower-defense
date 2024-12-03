using UnityEngine;

public class BottleAttackComponent : MonoBehaviour, ITowerAttackComponent
{
    [SerializeField] private GameObject bottlePrefab; 
    [SerializeField] private float throwForce = 8f;    

    public void Attack(Transform firePoint, float range, float damage, Transform target)
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position;
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