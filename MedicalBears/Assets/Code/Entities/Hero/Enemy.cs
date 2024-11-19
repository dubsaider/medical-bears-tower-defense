using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;

public class Enemy : Hero, IMeleeAttacker
{
    protected List<Vector3> path;
    protected int currentWaypointIndex = 0;
    private CoinManager coinManager;
    [SerializeField] private int reward;
    public Animator anim;

    [SerializeField] protected NavMeshAgent navMeshAgent;
    
    public float Range { get { return attackRange; } }
    public float Damage { get { return damage; } }

    void Start()
    {

	Animator animator = GetComponent<Animator>();
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
        
        if (navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.SetDestination(newPath.Last());
        }
    }

    private void Update()
    {
        if (isAlive)
        {
            Move();
            Attack();
        }
    }

    public void Attack()
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

        if (nearestTower != null && Vector3.Distance(transform.position, nearestTower.transform.position) <= attackRange)
        {
            nearestTower.GetComponent<Hero>().TakeDamage(Damage);
            Debug.Log($"Enemy {gameObject.name} attacks {nearestTower.name} at distance {shortestDistance}");
        }
    }

    public override void Move()
    {
        if (path != null && path.Count > 0 && currentWaypointIndex < path.Count)
        {
            Vector3 targetPosition = path[currentWaypointIndex];
            if (navMeshAgent.isActiveAndEnabled)
            {
                navMeshAgent.SetDestination(targetPosition);
            }

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            Vector3 targetPosition = transform.position + Vector3.down * 10f; 
            if (navMeshAgent.isActiveAndEnabled)
            {
                navMeshAgent.SetDestination(targetPosition);
            }
        }
    }

    public override void Die()
    {
        if (coinManager != null)
        {
            coinManager.AddCoins(reward); 
        }
        isAlive = false;
        Debug.Log($"Enemy {gameObject.name} dies");
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
            Debug.Log($"Enemy {gameObject.name} finds nearest tower {nearestTower.name}");
        }
    }

}