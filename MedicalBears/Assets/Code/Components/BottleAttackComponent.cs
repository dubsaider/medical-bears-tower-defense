using UnityEngine;

public class BottleAttackComponent : MonoBehaviour, ITowerAttackComponent
{
    [SerializeField] private GameObject bottlePrefab; 
    [SerializeField] private float throwForce = 5f;
    [SerializeField] private Transform firePoint;


    public void Attack(Transform firePoint, float range, float damage)
    {
        GameObject bottle = Instantiate(bottlePrefab, firePoint.position, Quaternion.identity);

        Bottle bottleScript = bottle.GetComponent<Bottle>();
        if (bottleScript != null)
        {
            bottleScript.Initialize(damage, range); 
        }

        Rigidbody2D rb = bottle.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(firePoint.up * throwForce, ForceMode2D.Impulse);
        }
    }
}
