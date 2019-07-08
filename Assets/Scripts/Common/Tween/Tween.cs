using System;
using System.Collections;
using UnityEngine;

namespace Common.Tween
{
    public class Tween
    {
        private Transform _target = null;
        private TransformType _type = TransformType.LocalPosition;
        private Vector3 _from = Vector3.zero;
        private Vector3 _to = Vector3.zero;
        private Action _callback = null;
        private AnimationCurve _curve = null;
        private float _duration = 0.0f;
        private float _delay = 0.0f;
        private int _loopCount = 0;
        private bool _isPlaying = false;
        private WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();

        public Tween(Transform target, TransformType type, Vector3 to, float duration, Action callback = null, EasingType easyType = EasingType.Linear, int loop = 0, float delay = 0)
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
                yield return _waitForEndOfFrame;
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
                    else if (_loopCount == 0) yield return _waitForEndOfFrame;
                    else
                    {
                        if (count >= _loopCount) yield return _waitForEndOfFrame;
                        else
                        {
                            this.Swap(ref _from, ref _to);
                            playTimer = 0;
                            count++;
                        }
                    }
                }
                else yield return _waitForEndOfFrame;
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
                case TransformType.LocalPosition:
                    from = _target.transform.localPosition;
                    break;
                case TransformType.Position:
                    from = _target.transform.position;
                    break;
                case TransformType.Scale:
                    from = _target.transform.localScale;
                    break;
                case TransformType.Rotation:
                    from = _target.transform.rotation.eulerAngles;
                    break;
                case TransformType.LocalRotation:
                    from = _target.transform.localRotation.eulerAngles;
                    break;
            }
            return from;
        }

        private void Lerp(Vector3 a, Vector3 b, float t)
        {
            switch (_type)
            {
                case TransformType.LocalPosition:
                    _target.transform.localPosition = Vector3.Lerp(a, b, t);
                    break;
                case TransformType.Position:
                    _target.transform.position = Vector3.Lerp(a, b, t);
                    break;
                case TransformType.Scale:
                    _target.transform.localScale = Vector3.Lerp(a, b, t);
                    break;
                case TransformType.Rotation:
                    _target.transform.rotation = Quaternion.Euler(Vector3.Lerp(a, b, t));
                    break;
                case TransformType.LocalRotation:
                    _target.transform.localRotation = Quaternion.Euler(Vector3.Lerp(a, b, t));
                    break;
            }
        }
    }
}