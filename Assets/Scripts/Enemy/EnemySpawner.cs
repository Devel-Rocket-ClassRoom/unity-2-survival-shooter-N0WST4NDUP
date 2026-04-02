using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyBase[] Prefabs;
    public Transform[] SpawnPoints;

    public int EnemyCapacity = 30;

    private float _spawnInterval = 3f;
    private float _timer = 3f;

    private List<EnemyBase> _enemies = new();

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_spawnInterval > _timer || _enemies.Count == EnemyCapacity) return;

        CreateEnemy();

        _timer = 0;
    }

    private void CreateEnemy()
    {
        var point = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        var enemy = Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], point.position, point.rotation);
        _enemies.Add(enemy);

        enemy.GetComponent<LivingEntity>().OnDead.AddListener(() => _enemies.Remove(enemy));
    }
}
