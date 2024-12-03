using System;
using System.Collections;
using Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Components
{
    public class MeleeAttackComponent : MonoBehaviour, IEnemyAttackComponent
    {
        public float Range => range;
        public float Damage => damage;
        public float AttackCooldown => attackCooldown;

        public Hero Target { get; private set; }
        public bool HasTarget => Target;
        public bool IsTargetInAttackRange =>
            HasTarget && Vector3.Distance(transform.position, Target.transform.position) <= Range;

        [SerializeField] private float range;
        [SerializeField] private float damage;
        [SerializeField] private float attackCooldown;
        [SerializeField] private float maxTargetPursuit;

        private WaitForSeconds FindTargetDelay => WaitFor.Seconds1;
        private WaitForSeconds FollowTargetDelay => WaitFor.Seconds01;
        private WaitForSeconds _attackDelay;
        
        private NavMeshAgent _navMeshAgent;

        public IEnumerator FindAndFollowTarget()
        {
            while (true)
            {
                if(!HasTarget)
                    TryFindTarget();
                
                while (HasTarget)
                {
                    if (!Target.IsAlive())
                        DropTarget();

                    if (HasTarget)
                        _navMeshAgent.SetDestination(Target.transform.position);
                    
                    yield return FollowTargetDelay;
                }

                yield return FindTargetDelay;
            }
        }
        
        public IEnumerator Attack()
        {
            while (true)
            {
                if (IsTargetInAttackRange)
                    Target.TakeDamage(Damage);
                
                yield return _attackDelay;
            }
        }

        private void TryFindTarget()
        {
            var units = GameObject.FindGameObjectsWithTag("FriendlyUnit");
            if (units.Length == 0)
                return;

            
            foreach (var unit in units)
            {
                if (unit.TryGetComponent(out Hero hero) && hero.IsAlive() && hero.pursuitCnt < maxTargetPursuit)
                {
                    Target = hero;
                    hero.pursuitCnt++;
                }
            }
        }

        private void DropTarget()
        {
            Target.pursuitCnt--;
            Target = null;
            _navMeshAgent.ResetPath();
        }

        private void Awake()
        {
            _attackDelay = new WaitForSeconds(AttackCooldown);
                
            _navMeshAgent = GetComponent<NavMeshAgent>();

            StartCoroutine(FindAndFollowTarget());
            StartCoroutine(Attack());
        }

        private void OnDisable()
        {
            DropTarget();
        }
    }
}