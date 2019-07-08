using System;
using UnityEngine;
using UnityEngine.Events;

public class TweenControl : MonoBehaviour
{
    [Serializable] public class Callback: UnityEvent {}

    public Vector3 origin = Vector3.zero;
    public Vector3 target = Vector3.zero;
    public float duration = 0.0f;
    public int loopCount = 0;
    public float delay = 0.0f;
    public bool isAuto = false;
    public TweenType tweenType = TweenType.LocalPosition;
    public EasingType easingType = EasingType.Linear;
    public Callback onComplete = null;

    private Tweener _tweener = new Tweener();

    private void Awake()
    {
        if (isAuto) this.Play();
    }

    public void Play()
    {
        this.Reset();

        _tweener.Play(
            transform, tweenType, target, duration, () => {
                onComplete?.Invoke();
            }, easingType, loopCount, delay);
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
        switch (tweenType)
        {
            case TweenType.LocalPosition:
                transform.localPosition = origin;
                break;
            case TweenType.Position:
                transform.position = origin;
                break;
            case TweenType.Scale:
                transform.localScale = origin;
                break;
            case TweenType.Rotation:
                transform.rotation = Quaternion.Euler(origin);
                break;
            case TweenType.LocalRotation:
                transform.localRotation = Quaternion.Euler(origin);
                break;
        }
    }
}