using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Enemy : Hero
{
    private List<Vector3> path;
    private int currentWaypointIndex = 0;

    public CoinManager coinManager;

    void Start()
    {
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f; 
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; 

        CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
        collider.isTrigger = true; 

        coinManager = Object.FindAnyObjectByType<CoinManager>();

        // Ищем ближайшую башню при старте
        FindNearestTower();
    }

    public void SetPath(List<Vector3> newPath)
    {
        path = newPath;
        currentWaypointIndex = 0;
    }

    private void Update()
    {
        Move();
    }

    public override void Attack()
    {
        Debug.Log("Enemy attacks!");
    }

    public override void Move()
    {
        if (path != null && path.Count > 0 && currentWaypointIndex < path.Count)
        {
            Vector3 targetPosition = path[currentWaypointIndex];
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
    }

    public override void Die()
    {
        if (coinManager != null)
        {
            coinManager.AddCoins(10); 
        }

        Destroy(gameObject);
    }

    public override int GetAttackPriority(Hero target)
    {
        return 1;
    }

    private void FindNearestTower()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        GameObject nearestTower = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject tower in towers)
        {
            float distanceToTower = Vector3.Distance(transform.position, tower.transform.position);
            if (distanceToTower < shortestDistance)
            {
                shortestDistance = distanceToTower;
                nearestTower = tower;
            }
        }

        if (nearestTower != null)
        {
            List<Vector3> newPath = new List<Vector3> { nearestTower.transform.position };
            SetPath(newPath);
        }
    }
}