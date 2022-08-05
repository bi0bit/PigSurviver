using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool
{
    private GameObject _gameObject;
    private int _capacity;
    private bool _createOnInit;
    private Transform _transform;

    private List<GameObject> _gameObjects;

    private List<GameObject> Inactive => _gameObjects.Where(obj => !obj.activeSelf).ToList();
    private bool HasCreatedInactive => Inactive.Count > 0;


    public ObjectPool(Transform transform, GameObject gameObject, int capacity, bool createOnInit)
    {
        _transform = transform;
        _gameObject = gameObject;
        _capacity = capacity;
        _createOnInit = createOnInit;
        InitObjectPool();
    }

    public void InitObjectPool()
    {
        _gameObjects = new List<GameObject>(_capacity);
        if (_createOnInit)
        {
            for (int i = 0; i < _capacity; i++)
            {
                var newObj = CreateNew(_transform.position);
                newObj.SetActive(false);
                _gameObjects.Add(newObj);
            }
        }
    }

    public void Release(GameObject obj)
    {
        if (_gameObjects.Contains(obj))
        {
            obj.SetActive(false);
        }
        else
        {
            Debug.LogWarning("GameObject is not part of object pool", obj);
        }
    }

    public T Get<T>(Vector3 position, bool forceCreate = false)
    {
        return Get(position, forceCreate).GetComponent<T>();
    }

    public GameObject Get(Vector3 position, bool forceCreate = false)
    {
        GameObject obj;
        if (!forceCreate && HasCreatedInactive)
        {
            obj = Inactive.First();
            obj.SetActive(true);
            obj.transform.position = position;
        }
        else
        {
            obj = CreateNew(position);
            _gameObjects.Add(obj);
        }

        return obj;
    }

    public bool Contains(GameObject gameObject) => _gameObjects.Contains(gameObject);

    private GameObject CreateNew(Vector3 position) => Object.Instantiate(_gameObject, position,
        Quaternion.identity, _transform);
}