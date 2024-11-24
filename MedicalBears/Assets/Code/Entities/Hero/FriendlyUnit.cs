using Code.Core;
using Code.Entities.Map;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// хилер должен вызываться из башни, поэтому в нём есть метод Init
// хотя не, пока независимым делаю

public class FriendlyUnit : Hero, ICorruptionHealer
{
    [SerializeField] private int cellFoundRadius;
    [SerializeField] private int healForce;

    private Vector3 HomePosition;
    private GameObject path;

    // для поиска клетки
    private GameObject newPath = null;
    private float dist = 0f;
    private float min_dist;

    private int width;
    private int height;

    private bool isActive = false;

    private NavMeshAgent navMeshAgent;

    public void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        width = CoreManager.Instance.GetWidth();
        height = CoreManager.Instance.GetHeight();

        HomePosition = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

        Controller();
    }

    private void Controller()
    {
        if (!isActive)
        {

            InvokeRepeating("FindNearestCorrupCell", 0f, 1f);
        }
        else
        {
            CancelInvoke("FindNearestCorrupCell");
            InvokeRepeating("HealCorrup", 1f, 1f);
        }
    }

    private void HealCorrup()
    {
        if (path.GetComponent<CellCorruptionHandler>().GetCorruptionLevel() == 0)
        {
            isActive = false;

            if (navMeshAgent.isActiveAndEnabled) navMeshAgent.SetDestination(HomePosition);

            CancelInvoke("HealCorrup");
            Controller();
        }
        else
        {
            float dist = Vector3.Distance(transform.position, path.transform.position);

            if (dist <= attackRange)
            {
                path.GetComponent<CellCorruptionHandler>().DecreaseCorruptionLevel(healForce);
            }
        }
    }

    public void HealCorruption(Corruption corruption)
    {
        // Логика лечения заражения
    }

    public override void Die()
    {
        // Логика смерти
        isAlive = false;
        Debug.Log($"Enemy {gameObject.name} dies");
        gameObject.SetActive(false);
    }

    public override void Move()
    {
        // Логика движения
    }

    public override int GetAttackPriority(Hero target)
    {
        return 0;
    }

    private void FindNearestCorrupCell()
    {
        min_dist = float.MaxValue;
        newPath = null;

        for (int y = (int)HomePosition.y - cellFoundRadius; y <= (int)HomePosition.y+cellFoundRadius; y++)
        {
            for (int x = (int)HomePosition.x - cellFoundRadius; x <= (int)HomePosition.x + cellFoundRadius; x++)
            {
                if (x > 0 && y > 0 && x < width && y < height)
                {
                    if (CoreManager.Instance.GetCell(x, y)?.GetComponent<CellCorruptionHandler>()?.GetCorruptionLevel() > 0)
                    {
                        dist = Vector3.Distance(HomePosition, new Vector3(x, y));

                        if (dist < min_dist)
                        {
                            min_dist = dist;

                            newPath = CoreManager.Instance.GetCell(x, y);
                        }
                    }
                }
            }
        }

        if (newPath)
        { 
            SetPath(newPath);
        }
    }

    private void SetPath(GameObject newPath)
    {
        path = newPath;
        isActive = true;

        if (navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.SetDestination(newPath.transform.position);
        }
        Controller();
    }
}