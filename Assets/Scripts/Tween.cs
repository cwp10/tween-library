using System;
using System.Collections;
using UnityEngine;

public class Tween
{
    private Transform _target = null;
    private TweenType _type = TweenType.LocalPosition;
    private Vector3 _from = Vector3.zero;
    private Vector3 _to = Vector3.zero;
    private Action _callback = null;
    private AnimationCurve _curve = null;
    private float _duration = 0.0f;
    private float _delay = 0.0f;
    private bool _isPlaying = false;

    public Tween(Transform target, TweenType type, Vector3 to, float duration, Action callback = null, EasingType easyType = EasingType.Linear, float delay = 0)
    {
        this._target = target;
        this._type = type;
        this._to = to;
        this._duration = duration;
        this._callback = callback;
        this._curve = CurveGenerator.Create(easyType);
        this._delay = delay;
    }

    public void AddListener(Action callback)
    {
        _callback += callback;
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
        float playTimer = 0.0f;
        float delayTimer = 0.0f;

        while (delayTimer <= this._delay)
        {
            if (_isPlaying) delayTimer += Time.deltaTime;
            yield return null;
        }

        while (playTimer <= this._duration)
        {
            Lerp(this._from, this._to, this._curve.Evaluate(playTimer / this._duration));
            if (_isPlaying) playTimer += Time.deltaTime;
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
