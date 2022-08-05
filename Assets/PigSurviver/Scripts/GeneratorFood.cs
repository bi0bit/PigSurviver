using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneratorFood : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _foodPlaces;

    [SerializeField]
    private Food _food;

    private ObjectPool _foodPool;

    private void Start()
    {
        _foodPool = new ObjectPool(transform, _food.gameObject, 1, true);
    }

    public Food Generate()
    {
        var place = _foodPlaces[Random.Range(0, _foodPlaces.Count)];
        return _foodPool.Get<Food>(place.position);
    }

    public void Realise(GameObject obj)
    {
        _foodPool.Release(obj);
    }
}
