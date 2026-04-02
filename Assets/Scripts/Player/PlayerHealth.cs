using UnityEngine;

public class PlayerHealth : LivingEntity
{
    public float StartingHealth;

    protected override void OnEnable()
    {
        MaxHP = StartingHealth;
        base.OnEnable();
    }

    private void Update()
    {
        Debug.Log(Health);
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
    }
}