using UnityEngine;

public class Main : MonoBehaviour
{
    private Tweener _tweener = new Tweener();

    void Start()
    {
        _tweener.Play(this, TweenType.LocalPosition, new Vector3(2, 2, 0), 3, callback: () => {
            _tweener.Play(this, TweenType.LocalPosition, new Vector3(1, -2, 0), 1, callback: () => {
                _tweener.Play(this, TweenType.Rotation, new Vector3(0, 180, 0), 3f, callback: OnEnd, easyType: EasingType.QuadEaseIn);
            }, easyType: EasingType.SineEaseIn, delay: 5.0f);
        }, easyType: EasingType.BounceEaseOutIn);
    }

    void OnEnd()
    {
        Debug.Log("OnEnd");
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