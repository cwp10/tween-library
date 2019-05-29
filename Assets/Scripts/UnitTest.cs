using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Tween;
using Tween.Curve;

public class UnitTest : MonoBehaviour
{
    private TweenPosition _tweenPosition = null;

    private void Awake()
    {
        _tweenPosition = GetComponent<TweenPosition>();
        _tweenPosition.Easing = EasingType.BounceEaseInOut;
        _tweenPosition.Option = Option.PingPong;
        _tweenPosition.From = new Vector3(0, 0, 0);
        _tweenPosition.To = new Vector3(3, 2, 0);
        _tweenPosition.Delay = 3.0f;
        _tweenPosition.Duration = 2.0f;
        _tweenPosition.OnComplete.AddListener(OnComplete);
        _tweenPosition.MoveTo();
    }

    public void OnComplete()
    {
        Debug.Log("OnComplete");
    }
}
