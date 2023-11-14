using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private int _maxEnemySpawn = 1;

    [SerializeField]
    private float _enemySpawnFrequency;

    [SerializeField]
    private float _spawnRadius;

    private float _currentEnemySpawnTime;
    private int _currentEnemySpawnCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Turn off placement support during gameplay
        if (spriteRenderer)
        {
            spriteRenderer.enabled = false;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentEnemySpawnCount >= _maxEnemySpawn)
        {
            return;
        }

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
