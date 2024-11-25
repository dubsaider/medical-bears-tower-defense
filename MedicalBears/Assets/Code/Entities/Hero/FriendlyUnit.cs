using Code.Core;
using Code.Entities.Map;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// хилер должен вызываться из башни, поэтому в нём есть метод Init
// хотя не, пока независимым делаю

public class FriendlyUnit : Hero, ICorruptionHealer
{
    [SerializeField] private int cellFoundRadius;
    [SerializeField] private int healForce;

    private Vector3 HomePosition;
    private GameObject _path;

    // для поиска клетки
    private float dist = 0f;
    private float min_dist;

    private int width;
    private int height;

    private bool isActive = false;

    private NavMeshAgent navMeshAgent;

    public void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;

        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        width = CoreManager.Instance.GetWidth();
        height = CoreManager.Instance.GetHeight();

        HomePosition = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

        StartCoroutine(FindNearestCorrupCell());
    }

    IEnumerator FindNearestCorrupCell()
    {
        navMeshAgent.SetDestination(HomePosition);
        while (true)
        {
            min_dist = float.MaxValue;

            for (int y = (int)HomePosition.y - cellFoundRadius; y <= (int)HomePosition.y + cellFoundRadius; y++)
            {
                for (int x = (int)HomePosition.x - cellFoundRadius; x <= (int)HomePosition.x + cellFoundRadius; x++)
                {
                    if (CoreManager.Instance.Map.TryGetCorruptedCell(new Vector2Int(x, y), out var cell))
                    {
                        dist = Vector3.Distance(HomePosition, new Vector3(x, y));

                        if (dist < min_dist)
                        {
                            min_dist = dist;

                            yield return new WaitUntil(() => isActive == false);

                            SetPath(cell.CorruptionHandler.gameObject);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator HealCorrup()
    {

        while (true)
        {

            if (_path.GetComponent<CellCorruptionHandler>().GetCorruptionLevel() == 0)
            {
                isActive = false;

                if (navMeshAgent.isActiveAndEnabled) navMeshAgent.SetDestination(HomePosition);
                StopCoroutine("HealCorrup");

            }
            else
            {
                float dist = Vector3.Distance(transform.position, _path.transform.position);

                if (dist <= attackRange)
                {
                    _path.GetComponent<CellCorruptionHandler>().DecreaseCorruptionLevel(healForce);
                }
            }

            yield return new WaitForSeconds(1);
        }
    }

    private void SetPath(GameObject newPath)
    {
        _path = newPath;
        isActive = true;

        navMeshAgent.SetDestination(newPath.transform.position);


        StartCoroutine("HealCorrup");

    }

    public override void Die()
    {
        // Логика смерти
        isAlive = false;
        Debug.Log($"Enemy {gameObject.name} dies");
        gameObject.SetActive(false);
    }

    public override void Move() { }

    public void HealCorruption(Corruption corruption) { }
    public override int GetAttackPriority(Hero target) {return 0;}


    private void OnDisable()
    {
        StopAllCoroutines();
    }
}