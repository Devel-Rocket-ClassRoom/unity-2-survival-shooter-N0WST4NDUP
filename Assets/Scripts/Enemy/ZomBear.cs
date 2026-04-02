using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ZomBear : EnemyBase, IMoveable
{
    private NavMeshAgent _agent;

    private float _lastAttack;

    protected override void Awake()
    {
        base.Awake();

        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = data.Speed;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _agent.enabled = true;
        _agent.isStopped = false;
        _agent.ResetPath();

        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 10f, NavMesh.AllAreas))
        {
            _agent.Warp(hit.position);
        }
    }

    protected override void UpdateTrace()
    {
        if (Target == null || Vector3.Distance(Target.transform.position, transform.position) > data.TraceDistance)
        {
            Target = null;
            CurrentStatus = Status.Idle;
            return;
        }

        if (Target != null && Vector3.Distance(Target.transform.position, transform.position) <= data.AttackDistance)
        {
            CurrentStatus = Status.Attack;
            return;
        }

        OnMove();
    }

    protected override void UpdateAttack()
    {
        if (Time.time > _lastAttack + data.AttackInterval)
        {
            _agent.isStopped = true;
            var target = Target.gameObject.GetComponent<IDamageable>();
            target.OnDamage(data.Damage, Target.transform.position, transform.position);
            _lastAttack = Time.time;
        }

        CurrentStatus = Status.Trace;
    }

    public void OnMove()
    {
        _agent.isStopped = false;
        _agent.SetDestination(Target.transform.position);
    }
}