using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Code.Core;
using Extensions;

public class Enemy : Hero
{
    [SerializeField] protected int reward;
    
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private Vector3 _targetCellPosition;
    private WaitForSeconds PatrollingDelay => WaitFor.Seconds1;
    private bool IsTargetCellReached => Vector3.Distance(transform.position, _targetCellPosition) < 1f;
    private bool IsNavMeshAgentHasPath => _navMeshAgent.hasPath;
    private bool IsNavMeshAgentUnderControl =>
        IsNavMeshAgentHasPath && _navMeshAgent.destination == _targetCellPosition 
        || !IsNavMeshAgentHasPath;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        
        StartCoroutine(Patrolling());
    }
    
    private IEnumerator Patrolling()
    {
        while (isAlive)
        {
            if (!IsNavMeshAgentUnderControl) 
                yield return PatrollingDelay;
        
            if (IsTargetCellReached || !IsNavMeshAgentHasPath)
                MoveToRandomCell();
            
            yield return PatrollingDelay;
        }
    }
    
    private void MoveToRandomCell()
    {
        _targetCellPosition = (Vector3Int)CoreManager.Instance.Map.GetRandomFreeFloorCell().Position;
        Move();
    }
    
    public override void Move()
    {
        _navMeshAgent.SetDestination(_targetCellPosition);
    }

    public override void Die()
    {
        CoreManager.Instance.BalanceMediator.AddKillReward(reward); //сомнительно, но окэй

        isAlive = false;
        gameObject.SetActive(false);
    }

    public override int GetAttackPriority(Hero target)
    {
        return 1;
    }

    
}