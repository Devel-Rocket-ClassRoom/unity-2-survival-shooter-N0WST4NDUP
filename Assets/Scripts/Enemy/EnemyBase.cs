using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : LivingEntity
{
    private readonly int HasTarget = Animator.StringToHash("HasTarget");

    public EnemyData data;

    public ParticleSystem BloodEffect;

    protected Animator _enemyAnimator;
    protected AudioSource _enemyAudioSource;
    private Collider _enemyCollider;

    public Transform Target;
    public LayerMask TargetLayer;

    private bool _isSinking;
    public float SinkSpeed = 1f;
    private Status _currentStatus;
    public Status CurrentStatus
    {
        get { return _currentStatus; }
        set
        {
            var prevStatus = _currentStatus;
            _currentStatus = value;

            switch (_currentStatus)
            {
                case Status.Idle:
                    _enemyAnimator.SetBool(HasTarget, false);
                    break;
                case Status.Trace:
                    _enemyAnimator.SetBool(HasTarget, true);
                    break;
                case Status.Attack:
                    _enemyAnimator.SetBool(HasTarget, false);
                    break;
                case Status.Die:
                    _enemyAnimator.SetTrigger("Die");
                    _enemyAudioSource.PlayOneShot(data.DeathSound);
                    break;
            }
        }
    }

    protected override void OnEnable()
    {
        this.MaxHP = data.MaxHP;
        _enemyCollider.enabled = true;

        base.OnEnable();
    }

    protected virtual void Awake()
    {
        _enemyAnimator = GetComponent<Animator>();
        _enemyAudioSource = GetComponent<AudioSource>();
        _enemyCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        switch (_currentStatus)
        {
            case Status.Idle:
                UpdateIdle();
                break;
            case Status.Trace:
                UpdateTrace();
                break;
            case Status.Attack:
                UpdateAttack();
                break;
            case Status.Die:
                Die();
                if (_isSinking)
                {
                    transform.position += Vector3.down * SinkSpeed * Time.deltaTime;
                }
                break;
        }
    }

    protected virtual void UpdateIdle()
    {
        if (Target != null && Vector3.Distance(Target.position, transform.position) < data.TraceDistance)
        {
            CurrentStatus = Status.Trace;
            return;
        }

        Target = FindTarget(data.TraceDistance);
    }

    protected abstract void UpdateTrace();

    protected abstract void UpdateAttack();

    protected override void Die()
    {
        if (IsDead) return;

        base.Die();

        CurrentStatus = Status.Die;
        _enemyCollider.enabled = false;

        var agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }
    }

    private Transform FindTarget(float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, TargetLayer);
        if (colliders.Length == 0)
        {
            return null;
        }

        var target = colliders.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).First();
        return target.transform;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);

        _enemyAudioSource.PlayOneShot(data.HitSound);
        BloodEffect.transform.position = hitPoint;
        BloodEffect.transform.forward = hitNormal;
        BloodEffect.Play();
    }

    private void StartSinking()
    {
        Destroy(gameObject, 1f);
        _isSinking = true;
    }

    public enum Status
    {
        Idle,
        Trace,
        Attack,
        Die,
    }
}