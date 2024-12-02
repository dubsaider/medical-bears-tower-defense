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

    private Vector3 _homePosition;

    private NavMeshAgent _navMeshAgent;

    private GameObject _path;

    private Animator _animator;

    public int ID { get; private set; }

    // для поиска клетки
    private float _dist = 0f;
    private float _min_dist;

    private bool _isActive = false;

    public void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = speed;

        _animator = GetComponent<Animator>();

        _homePosition = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

        DestroyObjectsHandler.Add(gameObject);

        StartCoroutine(FindNearestCorrupCell());
        StartCoroutine(AnimationController());
    }

    IEnumerator FindNearestCorrupCell()
    {
        _navMeshAgent.SetDestination(_homePosition);
        while (true)
        {
            _min_dist = float.MaxValue;

            for (int y = (int)_homePosition.y - cellFoundRadius; y <= (int)_homePosition.y + cellFoundRadius; y++)
            {
                for (int x = (int)_homePosition.x - cellFoundRadius; x <= (int)_homePosition.x + cellFoundRadius; x++)
                {
                    if (CoreManager.Instance.Map.TryGetCorruptedCell(new Vector2Int(x, y), out var cell))
                    {
                        _dist = Vector3.Distance(_homePosition, new Vector3(x, y));

                        if (_dist < _min_dist)
                        {
                            _min_dist = _dist;

                            yield return new WaitUntil(() => _isActive == false);

                            SetPath(cell.CorruptionHandler.gameObject);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void SetPath(GameObject newPath)
    {
        _path = newPath;
        _isActive = true;

        _navMeshAgent.SetDestination(newPath.transform.position);

        StartCoroutine("HealCorrup");

    }

    IEnumerator HealCorrup()
    {

        while (true)
        {

            if (_path.GetComponent<CellCorruptionHandler>().GetCorruptionLevel() == 0)
            {
                _isActive = false;
                _animator.SetBool("isClean", false);
                if (_navMeshAgent.isActiveAndEnabled) _navMeshAgent.SetDestination(_homePosition);
                StopCoroutine("HealCorrup");

            }
            else
            {

                float dist = Vector3.Distance(transform.position, _path.transform.position);

                if (dist <= attackRange)
                {
                    _animator.SetBool("isClean", true);
                    _path.GetComponent<CellCorruptionHandler>().DecreaseCorruptionLevel(healForce);
                }
            }

            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator AnimationController()
    {
        while (true)
        {
            if (_navMeshAgent.velocity.magnitude > 0.5f)
            {
                _animator.SetBool("isRun", true);
            }
            else
            {
                _animator.SetBool("isRun", false);
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    public override void Die()
    {
        // Логика смерти
        isAlive = false;
        _animator.SetBool("isDie", true);

        StopAllCoroutines();
        _navMeshAgent.Stop();

        Debug.Log($"Enemy {gameObject.name} dies");
        CoreEventsProvider.HealerUnitHasDie.Invoke(ID);
        //gameObject.SetActive(false);
    }

    public override void Move() { }

    public void SetID(int val)
    {
        ID = val;
    }

    public void HealCorruption(Corruption corruption) { }
    public override int GetAttackPriority(Hero target) {return 0;}

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}