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
        Transform _target = null;

        private TweenType _type = TweenType.LocalPosition;
        private Vector3 _from = Vector3.zero;
        private Vector3 _to = Vector3.zero;
        private Action _callback = null;
        private AnimationCurve _curve = null;
        private float _duration = 0.0f;

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

        public IEnumerator Execution()
        {
            this._from = GetFrom();
            float timer = 0.0f;

            while (timer <= this._duration)
            {
                Lerp(this._from, this._to, this._curve.Evaluate(timer / this._duration));
                timer += Time.deltaTime;
                yield return null;
            }

            Lerp(this._to, this._to, 0);

            if (_callback != null)
            {
                _callback();
                _callback = null;
            }
        }

        private Vector3 GetFrom()
        {
            Vector3 from = Vector3.zero;

            switch (this._type)
            {
                case TweenType.LocalPosition:
                    from = this._target.localPosition;
                    break;
                case TweenType.Position:
                    from = this._target.position;
                    break;
                case TweenType.Scale:
                    from = this._target.localScale;
                    break;
                case TweenType.Rotation:
                    from = this._target.rotation.eulerAngles;
                    break;
                case TweenType.LocalRotation:
                    from = this._target.localRotation.eulerAngles;
                    break;
            }
            return from;
        }

        private void Lerp(Vector3 a, Vector3 b, float t)
        {
            switch (this._type)
            {
                case TweenType.LocalPosition:
                    this._target.localPosition = Vector3.Lerp(a, b, t);
                    break;
                case TweenType.Position:
                    this._target.position = Vector3.Lerp(a, b, t);
                    break;
                case TweenType.Scale:
                    this._target.localScale = Vector3.Lerp(a, b, t);
                    break;
                case TweenType.Rotation:
                    this._target.rotation = Quaternion.Euler(Vector3.Lerp(a, b, t));
                    break;
                case TweenType.LocalRotation:
                    this._target.localRotation = Quaternion.Euler(Vector3.Lerp(a, b, t));
                    break;
            }
        }
    }

    private Queue<Tween> _tweenQueue = new Queue<Tween>();
    private bool _isReady = true;
    private Tween _currentTween = null;

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

    private void Next()
    {
        if (_tweenQueue.Count > 0 && _isReady)
        {
            _isReady = false;
            _currentTween = _tweenQueue.Dequeue();
            CoroutineHandler.StartStaticCoroutine(_currentTween.Execution());
        }
    }

    private void OnTweenEnd()
    {
        _isReady = true;
        this.Next();
    }
}