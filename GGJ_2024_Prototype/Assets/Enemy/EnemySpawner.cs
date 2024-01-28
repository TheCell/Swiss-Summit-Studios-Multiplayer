using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab = null;
    [SerializeField] float _spawnInterval = 5f;
    [SerializeField] int _maxEnemies = 2;
    [SerializeField] private List<GameObject> _spawnedEnemies = new List<GameObject>();
    [SerializeField] private ParticleSystem[] particleSystems = new ParticleSystem[0];

    private float _spawnTimestamp;
    private float _checkOnSpawnedEnemiesTimestamp;
    private bool _isSpawnFree = true;

    public void Update()
    {
        CheckOnSpawnedEnemies();

        if (_isSpawnFree && _spawnTimestamp + _spawnInterval < Time.time)
        {
            _spawnTimestamp = Time.time;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (_spawnedEnemies.Count >= _maxEnemies)
        {
            return;
        }

        var enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        foreach (var particleSystem in particleSystems)
        {
            particleSystem.Play();
        }
        _spawnedEnemies.Add(enemy);
    }

    private void CheckOnSpawnedEnemies()
    {
        if (_checkOnSpawnedEnemiesTimestamp + 0.5f < Time.time)
        {
            return;
        }

        _checkOnSpawnedEnemiesTimestamp = Time.time;

        for (var i = _spawnedEnemies.Count - 1; i >= 0; i--)
        {
            if (_spawnedEnemies[i] == null)
            {
                _spawnedEnemies.RemoveAt(i);
            }
        }

        _isSpawnFree = true;
        foreach (var enemy in _spawnedEnemies)
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) < 5f)
            {
                _isSpawnFree = false;
                break;
            }
        }
    }

    //private int objectsInTrigger = 0;

    //public void OnTriggerEnter(Collider other)
    //{
    //    objectsInTrigger++;
    //    Debug.Log($"there are currently {objectsInTrigger} in the trigger");
    //}

    //public void OnTriggerExit(Collider other)
    //{
    //    objectsInTrigger--;
    //    Debug.Log($"there are currently {objectsInTrigger} in the trigger");
    //}
}
