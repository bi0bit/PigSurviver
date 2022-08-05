using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyDelay : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSecondsRealtime(8);
        if (transform.parent.TryGetComponent(out ObjectPoolWrap pool))
        {
            pool.Realise(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
