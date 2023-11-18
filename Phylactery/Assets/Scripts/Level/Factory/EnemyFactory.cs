using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField]
    protected GameObject _enemyPrefab;

    [SerializeField]
    protected int _maxEnemySpawn = 1;

    [SerializeField]
    protected float _enemySpawnFrequency;

    [SerializeField]
    protected float _spawnRadius;

    protected float _currentEnemySpawnTime;
    protected int _currentEnemySpawnCount = 0;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Turn off placement support during gameplay
        if (spriteRenderer)
        {
            spriteRenderer.enabled = false;
        }    
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_currentEnemySpawnCount >= _maxEnemySpawn)
        {
            return;
        }

        SpawnEnemy();
    }

    protected virtual void SpawnEnemy()
    {
        if (_currentEnemySpawnTime == 0)
        {
            GameObject enemy = Instantiate(_enemyPrefab, transform.position + new Vector3(Random.RandomRange(-_spawnRadius, _spawnRadius), Random.RandomRange(-_spawnRadius, _spawnRadius), 0.0f), Quaternion.identity);
            _currentEnemySpawnCount++;
        }
        _currentEnemySpawnTime += Time.deltaTime;

        if (_currentEnemySpawnTime >= _enemySpawnFrequency)
        {
            _currentEnemySpawnTime = 0;
        }
    }
}
