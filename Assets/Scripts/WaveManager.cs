using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Vector3 _spawnPoint;
    [SerializeField] private EnemyBehaviour[] _enemiesPrefabs;
    [SerializeField] private float _minSpawnDelay, _maxSpawnDelay;

    private int _lastSpawn;
    private float _delay;
    private bool _stop;

    // Start is called before the first frame update
    void Start()
    {
        _lastSpawn = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (_stop)
            return;
        if (_delay < Time.time)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        _delay = Time.time + Random.Range(_minSpawnDelay, _maxSpawnDelay);

        int index = Random.Range(0, _enemiesPrefabs.Length);
        while (index == _lastSpawn)
        {
            index = Random.Range(0, _enemiesPrefabs.Length);
        }

        Vector3 pos = new Vector3(_spawnPoint.x * Mathf.Sin(Time.time), _spawnPoint.y, _spawnPoint.z);
        Instantiate(_enemiesPrefabs[index], pos, Quaternion.identity, transform);
    }
}
