using UnityEngine;
using Common.Tween;

public class TweenDemo : MonoBehaviour
{
    private Tweener _tweener = new Tweener();

    void Start()
    {
        _tweener.Play(this, TransformType.LocalPosition, new Vector3(2, 3, 0), 1, ()=> {
            _tweener.Play(this, TransformType.LocalPosition, new Vector3(0, -2, 0), 0.5f, ()=> {
                Debug.Log("tween end");
            }, EasingType.QuadEaseIn, 5, 2);
        }, EasingType.BounceEaseOutIn, 2, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _tweener.Pause();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _tweener.Resume();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _tweener.Stop();
        }
    }
}