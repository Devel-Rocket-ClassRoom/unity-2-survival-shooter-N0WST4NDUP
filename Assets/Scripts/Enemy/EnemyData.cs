using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public AudioClip HitSound;
    public AudioClip DeathSound;

    public float MaxHP = 100f;
    public float Damage = 20f;
    public float Speed = 2f;
    public float TraceDistance = 20f;
    public float AttackInterval = 1f;
    public float AttackDistance = 1f;
}
