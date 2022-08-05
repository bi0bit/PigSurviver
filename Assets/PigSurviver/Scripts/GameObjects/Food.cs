using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Food : MonoBehaviour
{

    private bool _isEaten;

    private AudioSource _audio;

    private event UnityAction EventEaten;
    
    public event UnityAction Eaten
    {
        add => EventEaten += value;
        remove => EventEaten -= value;
    }

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        DOTween.Sequence()
            .Append(transform.DOMoveY(.15f, 1f).SetRelative())
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnEnable()
    {
        _isEaten = false;
    }

    private void OnDisable()
    {
        EventEaten = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isEaten) return;
        if(!other.TryGetComponent(out Pig _)) return;
        _isEaten = true;
        AudioSource.PlayClipAtPoint(_audio.clip, transform.position);
        EventEaten?.Invoke();
        GetComponentInParent<GeneratorFood>().Realise(gameObject);
    }
}
