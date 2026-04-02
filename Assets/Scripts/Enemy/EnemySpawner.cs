using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyBase[] Prefabs;
    public Transform[] SpawnPoints;

    private List<EnemyBase> _enemies = new();
}
