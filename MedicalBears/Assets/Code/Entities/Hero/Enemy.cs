using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;

public class Enemy : Hero
{
    private List<Vector3> path;
    private int currentWaypointIndex = 0;
    private CoinManager coinManager;
    [SerializeField] private int reward;
    
    [SerializeField] private NavMeshAgent navMeshAgent;
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f; 
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; 

        CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
        collider.isTrigger = true; 

        coinManager = Object.FindAnyObjectByType<CoinManager>();

        InvokeRepeating("FindNearestTower", 1f, 1f);
    }

    public void SetPath(List<Vector3> newPath)
    {
        path = newPath;
        currentWaypointIndex = 0;
        
        navMeshAgent.SetDestination(newPath.Last());
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
            navMeshAgent.SetDestination(targetPosition);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            Vector3 targetPosition = transform.position + Vector3.down * 10f; 
            navMeshAgent.SetDestination(targetPosition);
        }
    }

    public override void Die()
    {
        if (coinManager != null)
        {
            coinManager.AddCoins(reward); 
        }

        gameObject.SetActive(false);
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