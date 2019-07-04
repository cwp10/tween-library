using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TweenType
{
    Position = 0,
    LocalPosition,
    Rotation,
    LocalRotation,
    Scale,
}

public class Tweener
{
    public bool IsPlaying { get; private set; }

    private Queue<Tween> _tweenQueue = new Queue<Tween>();
    private bool _isReady = true;
    private Tween _currentTween = null;
    private IEnumerator _coroutine = null;

    public void Play(MonoBehaviour target, TweenType type, Vector3 to, float duration, Action callback = null, EasingType easyType = EasingType.Linear, float delay = 0)
    {
        this.Play(target.transform, type, to, duration, callback, easyType, delay);
    }

    public void Play(GameObject target, TweenType type, Vector3 to, float duration, Action callback = null, EasingType easyType = EasingType.Linear, float delay = 0)
    {
        this.Play(target.transform, type, to, duration, callback, easyType, delay);
    }

    public void Play(Transform target, TweenType type, Vector3 to, float duration, Action callback = null, EasingType easyType = EasingType.Linear, float delay = 0)
    {
        Tween tween = new Tween(target, type, to, duration, callback, easyType, delay);
        this.Play(tween);
    }

    public void Play(Tween tween)
    {
        tween.AddListener(OnTweenEnd);
        _tweenQueue.Enqueue(tween);
        this.Next();
    }

    public void Pause()
    {
        if (_currentTween != null) _currentTween.Pause();
    }

    public void Resume()
    {
        if (_currentTween != null) _currentTween.Resume();
    }

    public void Stop()
    {
        if (_coroutine != null)
        {
            _tweenQueue.Clear();
            CoroutineHandler.StopStaticCoroutine(_coroutine);
            _isReady = true;
        }
    }

    private void Next()
    {
        if (_tweenQueue.Count > 0 && _isReady)
        {
            _isReady = false;
            _currentTween = _tweenQueue.Dequeue();
            _coroutine = _currentTween.Execution();
            CoroutineHandler.StartStaticCoroutine(_coroutine);
        }
    }

    private void OnTweenEnd()
    {
        _isReady = true;
        this.Next();
    }
}