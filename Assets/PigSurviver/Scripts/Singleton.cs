using System;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    
    protected static Singleton<T> _instance;

    public static T Instance => _instance as T;

    protected void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }
        _instance = this;
        
    }
}