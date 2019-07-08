using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Tween
{
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
        private Queue<Tween> _tweenQueue = new Queue<Tween>();
        private bool _isReady = true;
        private Tween _currentTween = null;
        private IEnumerator _coroutine = null;

        public bool IsPlaying { get; private set; }

        public void Play(MonoBehaviour target, TweenType type, Vector3 to, float duration, Action callback = null, EasingType easyType = EasingType.Linear, int loopCount = 0, float delay = 0)
        {
            this.Play(target.transform, type, to, duration, callback, easyType, loopCount, delay);
        }

        public void Play(GameObject target, TweenType type, Vector3 to, float duration, Action callback = null, EasingType easyType = EasingType.Linear, int loopCount = 0, float delay = 0)
        {
            this.Play(target.transform, type, to, duration, callback, easyType, loopCount, delay);
        }

        public void Play(Transform target, TweenType type, Vector3 to, float duration, Action callback = null, EasingType easyType = EasingType.Linear, int loopCount = 0, float delay = 0)
        {
            Tween tween = new Tween(target.transform, type, to, duration, callback, easyType, loopCount, delay);
            this.Play(tween);
        }

        public void Play(Tween tween)
        {
            tween.AddListener(OnComplete);
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

        private void OnComplete()
        {
            _isReady = true;
            _currentTween = null;
            this.Next();
        }
    }
}