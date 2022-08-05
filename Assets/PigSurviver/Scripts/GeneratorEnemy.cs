using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneratorEnemy : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _enemyPlaces;

    [SerializeField]
    private List<Enemy> _enemies;

    private List<ObjectPool> _enemiesPools;

    private void Start()
    {
        _enemiesPools = new List<ObjectPool>(_enemies.Count);
        _enemies.ForEach(enemy =>
        {
            _enemiesPools.Add(new ObjectPool(transform, enemy.gameObject, 3, true));
        });
    }

    public Enemy Generate()
    {
        var place = _enemyPlaces[Random.Range(0, _enemyPlaces.Count)];
        var enemyPool = _enemiesPools[Random.Range(0, _enemies.Count)];
        return enemyPool.Get<Enemy>(place.position);
    }

    public void Release(GameObject gameObject)
    {
        var objectPool = _enemiesPools.Find(pool => pool.Contains(gameObject));
        objectPool?.Release(gameObject);
    }
}
