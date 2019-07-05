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
    private int _loopCount = 0;
    private bool _isPlaying = false;

    public Tween(Transform target, TweenType type, Vector3 to, float duration, Action callback = null, EasingType easyType = EasingType.Linear, int loop = 0, float delay = 0)
    {
        this._target = target;
        this._type = type;
        this._to = to;
        this._duration = duration;
        this._callback = callback;
        this._curve = CurveGenerator.Create(easyType);
        this._delay = delay;
        this._loopCount = loop;
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
        _from = GetFrom();

        float playTimer = 0.0f;
        float delayTimer = 0.0f;
        float count = 0;

        while (delayTimer <= _delay)
        {
            if (_isPlaying) delayTimer += Time.deltaTime;
            yield return null;
        }

        do 
        {
            this.Lerp(_from, _to, _curve.Evaluate(playTimer / _duration));

            if (_isPlaying) playTimer += Time.deltaTime;

            if (playTimer > _duration)
            {
                if (_loopCount < 0)
                {
                    this.Swap(ref _from, ref _to);
                    playTimer = 0;
                }
                else if (_loopCount == 0) yield return null;
                else
                {
                    if (count >= _loopCount) yield return null;
                    else
                    {
                        this.Swap(ref _from, ref _to);
                        playTimer = 0;
                        count++;
                    }
                }
            }
            else yield return null;
        } while (playTimer <= _duration);

        Lerp(_to, _to, 0);

        _isPlaying = false;
        _callback?.Invoke();
        _callback = null;
    }

    private void Swap(ref Vector3 from, ref Vector3 to)
    {
        Vector3 temp = from;
        from = to;
        to = temp;
    }

    private Vector3 GetFrom()
    {
        Vector3 from = Vector3.zero;

        switch (_type)
        {
            case TweenType.LocalPosition:
                from = _target.transform.localPosition;
                break;
            case TweenType.Position:
                from = _target.transform.position;
                break;
            case TweenType.Scale:
                from = _target.transform.localScale;
                break;
            case TweenType.Rotation:
                from = _target.transform.rotation.eulerAngles;
                break;
            case TweenType.LocalRotation:
                from = _target.transform.localRotation.eulerAngles;
                break;
        }
        return from;
    }

    private void Lerp(Vector3 a, Vector3 b, float t)
    {
        switch (_type)
        {
            case TweenType.LocalPosition:
                _target.transform.localPosition = Vector3.Lerp(a, b, t);
                break;
            case TweenType.Position:
                _target.transform.position = Vector3.Lerp(a, b, t);
                break;
            case TweenType.Scale:
                _target.transform.localScale = Vector3.Lerp(a, b, t);
                break;
            case TweenType.Rotation:
                _target.transform.rotation = Quaternion.Euler(Vector3.Lerp(a, b, t));
                break;
            case TweenType.LocalRotation:
                _target.transform.localRotation = Quaternion.Euler(Vector3.Lerp(a, b, t));
                break;
        }
    }
}
