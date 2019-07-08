using System;
using UnityEngine;
using UnityEngine.Events;

public class TweenControl : MonoBehaviour
{
    [Serializable] public class Callback: UnityEvent {}

    [SerializeField] private Transform target_ = null;
    [SerializeField] private Vector3 from_ = Vector3.zero;
    [SerializeField] private Vector3 to_ = Vector3.zero;
    [SerializeField] private float duration_ = 0.0f;
    [SerializeField] private int loopCount_ = 0;
    [SerializeField] private float delay_ = 0.0f;
    [SerializeField] private bool isAuto = false;
    [SerializeField] private TweenType tweenType_ = TweenType.LocalPosition;
    [SerializeField] private EasingType easingType_ = EasingType.Linear;
    [SerializeField] private Callback onComplete_ = null;

    private Tweener _tweener = new Tweener();

    private void Awake()
    {
        if (isAuto) this.Play();
    }

    public void SetTarget(Transform target)
    {
        this.target_ = target;
    }

    public void SetFrom(Vector3 from)
    {
        this.from_ = from;
    }

    public void SetTo(Vector3 to)
    {
        this.to_ = to;
    }

    public void SetDuration(float time)
    {
        this.duration_ = time;
    }

    public void SetDelay(float time)
    {
        this.delay_ = time;
    }

    public void SetLoopCount(int count)
    {
        this.loopCount_ = count;
    }

    public void SetTweenType(TweenType tweenType)
    {
        this.tweenType_ = tweenType;
    }

    public void SetEasingType(EasingType easingType)
    {
        this.easingType_ = easingType;
    }

    public void SetCompleteCallback(UnityAction action)
    {
        this.onComplete_.AddListener(action);
    }

    public void Play()
    {
        this.Reset();

        Debug.Assert(target_ != null, "target is null");

        _tweener.Play(
            target_, tweenType_, to_, duration_, () => {
                onComplete_?.Invoke();
                onComplete_ = null;
            }, easingType_, loopCount_, delay_);
    }

    public void Pause()
    {
        _tweener.Pause();
    }

    public void Resume()
    {
        _tweener.Resume();
    }

    public void Stop()
    {
        _tweener.Stop();
    }

    private void Reset()
    {
        switch (tweenType_)
        {
            case TweenType.LocalPosition:
                transform.localPosition = from_;
                break;
            case TweenType.Position:
                transform.position = from_;
                break;
            case TweenType.Scale:
                transform.localScale = from_;
                break;
            case TweenType.Rotation:
                transform.rotation = Quaternion.Euler(from_);
                break;
            case TweenType.LocalRotation:
                transform.localRotation = Quaternion.Euler(from_);
                break;
        }
    }
}