using UnityEngine;

public class FXProvider : Singleton<FXProvider>
{
    [SerializeField] private ObjectPoolWrap _ashPool;
    [SerializeField] private ObjectPoolWrap _explosionPool;

    public static ObjectPoolWrap AshPool => Instance._ashPool;

    public static ObjectPoolWrap ExplosionPool => Instance._explosionPool;
}