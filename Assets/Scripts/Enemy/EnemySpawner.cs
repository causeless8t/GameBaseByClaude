using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    [Min(0)]
    private int _maxEnemyCount = 3;

    [SerializeField]
    [Min(0f)]
    private float _spawnInterval = 3f;

    private int _currentEnemyCount;
    private float _nextSpawnTime;

    private void Update()
    {
        if (_currentEnemyCount >= _maxEnemyCount)
        {
            return;
        }

        if (Time.time < _nextSpawnTime)
        {
            return;
        }

        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        var enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);

        var health = enemy.GetComponent<Health>();
        health.OnDied += () =>
        {
            _currentEnemyCount--;
        };

        _currentEnemyCount++;
        _nextSpawnTime = Time.time + _spawnInterval;
    }
}
