using UnityEngine;
using UnityEngine.Events;
using System;
using Tween.Curve;

namespace Tween
{
    public abstract class TweenObject : MonoBehaviour
    {
        [Serializable] public class TweenComplete : UnityEvent { }
        [SerializeField] protected AnimationCurve curve_ = null;
        [SerializeField] protected EasingType _easingType = EasingType.Linear;
        [SerializeField] private Option _option = Option.Once;
        [SerializeField] private float _delay = 0.0f;
        [SerializeField] private float _duration = 1.0f;
        [SerializeField] private bool _autoPlay = false;
        [SerializeField] private bool _playing = false;

        [SerializeField] private Vector3 _origin = Vector3.one;
        [SerializeField] private Vector3 _target = Vector3.one;
        
        public bool customCurve = false;
        public TweenComplete OnComplete;

        public EasingType Easing
        {
            protected get { return _easingType; }
            set { _easingType = value; }
        }

        public Option Option
        {
            protected get { return _option; }
            set { _option = value; }
        }

        public Vector3 From
        {
            get { return _origin; }
            set {_origin = value; }
        }

        public Vector3 To
        {
            get { return _target; }
            set {_target = value; }
        }

        public float Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public float Delay
        {
            get { return _delay; }
            set { _delay = value; }
        }

        public bool IsPlay
        {
            get { return _playing; }
        }

        public virtual void Play()
        {
            this._playing = true;
        }

        public virtual void Stop()
        {
            this._playing = false;
        }

        public virtual void Pause()
        {
            this._playing = false;
        }

        public virtual void Resume()
        {
            this._playing = true;
        }

        public void SetOption(Option option)
        {
            this._option = option;
        }

        protected virtual void Awake()
        {
            if(!customCurve) 
            {
                curve_ = CurveGenerator.Create(_easingType);
            }
            
            if (_autoPlay)
            {
                this.Play();
            }
        }
    }

}