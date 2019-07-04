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
    public class Tween
    {
        private Transform _target = null;
        private TweenType _type = TweenType.LocalPosition;
        private Vector3 _from = Vector3.zero;
        private Vector3 _to = Vector3.zero;
        private Action _callback = null;
        private AnimationCurve _curve = null;
        private float _duration = 0.0f;
        private bool _isPlaying = false;

        public Tween(Transform target, TweenType type, Vector3 to, float duration, Action cb = null, EasingType easyType = EasingType.Linear)
        {
            this._target = target;
            this._type = type;
            this._to = to;
            this._duration = duration;
            this._callback = cb;
            this._curve = CurveGenerator.Create(easyType);
        }

        public void AddListener(Action cb)
        {
            _callback += cb;
        }

        public bool IsPlaying()
        {
            return _isPlaying;
        }

        public void Pause()
        {
            _isPlaying = false;
        }

        public void Resume()
        {
            _isPlaying = true;
        }

        public IEnumerator Execution()
        {
            _isPlaying = true;
            this._from = GetFrom();
            float timer = 0.0f;

            while (timer <= this._duration)
            {
                Lerp(this._from, this._to, this._curve.Evaluate(timer / this._duration));
                if (_isPlaying) timer += Time.deltaTime;
                yield return null;
            }

            Lerp(this._to, this._to, 0);

            _isPlaying = false;
            _callback?.Invoke();
            _callback = null;
        }

        private Vector3 GetFrom()
        {
            Vector3 from = Vector3.zero;

            switch (this._type)
            {
                case TweenType.LocalPosition:
                    from = this._target.transform.localPosition;
                    break;
                case TweenType.Position:
                    from = this._target.transform.position;
                    break;
                case TweenType.Scale:
                    from = this._target.transform.localScale;
                    break;
                case TweenType.Rotation:
                    from = this._target.transform.rotation.eulerAngles;
                    break;
                case TweenType.LocalRotation:
                    from = this._target.transform.localRotation.eulerAngles;
                    break;
            }
            return from;
        }

        private void Lerp(Vector3 a, Vector3 b, float t)
        {
            switch (this._type)
            {
                case TweenType.LocalPosition:
                    this._target.transform.localPosition = Vector3.Lerp(a, b, t);
                    break;
                case TweenType.Position:
                    this._target.transform.position = Vector3.Lerp(a, b, t);
                    break;
                case TweenType.Scale:
                    this._target.transform.localScale = Vector3.Lerp(a, b, t);
                    break;
                case TweenType.Rotation:
                    this._target.transform.rotation = Quaternion.Euler(Vector3.Lerp(a, b, t));
                    break;
                case TweenType.LocalRotation:
                    this._target.transform.localRotation = Quaternion.Euler(Vector3.Lerp(a, b, t));
                    break;
            }
        }

        
    }

    public bool IsPlaying { get; private set; }

    private Queue<Tween> _tweenQueue = new Queue<Tween>();
    private bool _isReady = true;
    private Tween _currentTween = null;
    private IEnumerator _coroutine = null;

    public void Add(MonoBehaviour target, TweenType type, Vector3 to, float duration, Action cb = null, EasingType easyType = EasingType.Linear)
    {
        this.Add(target.transform, type, to, duration, cb, easyType);
    }

    public void Add(GameObject target, TweenType type, Vector3 to, float duration, Action cb = null, EasingType easyType = EasingType.Linear)
    {
        this.Add(target.transform, type, to, duration, cb, easyType);
    }

    public void Add(Transform target, TweenType type, Vector3 to, float duration, Action cb = null, EasingType easyType = EasingType.Linear)
    {
        Tween tween = new Tween(target, type, to, duration, cb, easyType);
        this.Add(tween);
    }

    public void Add(Tween tween)
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