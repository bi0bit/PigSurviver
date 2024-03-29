﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] 
    private float _explosionDelay;

    [SerializeField] 
    private float _explosionRadius;

    [SerializeField] 
    private GameObject _explosionPrefab;

    private AudioSource _audio; 
    
    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StartCoroutine(StartDetonate(_explosionDelay));
    }

    private IEnumerator StartDetonate(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        _audio.Play();
        Camera.main.DOShakePosition(.4f, .3f, 4, 120f).SetAutoKill(true);
        DealDamage();
        yield return ExplosionEffect();
        GetComponentInParent<ObjectPoolWrap>().Realise(gameObject);
    }

    private void DealDamage()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Enemy enemy))
            {
                enemy.ToDamage(1);
            }
        }
    }

    private IEnumerator ExplosionEffect()
    {
        var explosion = FXProvider.ExplosionPool.Get(transform.position);
        explosion.transform.DOScale(new Vector3(1f, 1f), .5f)
            .From(Vector3.one)
            .SetEase(Ease.Flash)
            .OnComplete(() =>
            {
                FXProvider.ExplosionPool.Realise(explosion);
            });
        yield return new WaitForSecondsRealtime(0.5f);
    }
    


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
#endif
    
}
