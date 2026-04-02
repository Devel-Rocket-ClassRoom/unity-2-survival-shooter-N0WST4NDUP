using UnityEngine;
using UnityEngine.Events;

public abstract class LivingEntity : MonoBehaviour, IDamageable
{
    public float MaxHP { get; protected set; }
    public float Health { get; protected set; }
    public bool IsDead { get; protected set; }

    public UnityEvent OnDead;

    protected virtual void OnEnable()
    {
        Health = MaxHP;
        IsDead = false;
    }

    public virtual void OnHealed(float amount)
    {
        Health = Mathf.Min(Health + amount, MaxHP);
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
    }

    protected virtual void Die()
    {
        IsDead = true;
        OnDead?.Invoke();
    }
}