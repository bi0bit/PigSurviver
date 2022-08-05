using System;
using UnityEngine;

public class ObjectPoolWrap : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private int _startCapacity;
    [SerializeField] private bool _createOnInit;
    
    private ObjectPool _objectPool;

    private void Start()
    {
        _objectPool = new ObjectPool(transform, _gameObject, _startCapacity, _createOnInit); 
    }
    
    public GameObject Get(Vector3 position, bool forceCreate = false)
    {
        return _objectPool.Get(position, forceCreate);
    }

    public T Get<T>(Vector3 position, bool forceCreate = false)
    {
        return _objectPool.Get<T>(position, forceCreate);
    }

    public void Realise(GameObject gameObject)
    {
        _objectPool.Release(gameObject);
    }
}