using UnityEngine;

public interface IDamageable
{
    void OnDamage(float damage, Vector3 position, Vector3 hitPosition);
}