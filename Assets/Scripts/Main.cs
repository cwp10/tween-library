using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        Tweener tweener = new Tweener();
        tweener.Add(this.transform, TweenType.LocalPosition, new Vector3(2, 2, 0), 3, cb: OnEnd, easyType: EasingType.BounceEaseOutIn);
        tweener.Add(this.transform, TweenType.LocalPosition, new Vector3(1, -2, 0), 1, cb: OnEnd, easyType: EasingType.SineEaseIn);
        tweener.Add(this.transform, TweenType.Rotation, new Vector3(0, 180, 0), 3f, cb: OnEnd, easyType: EasingType.QuadEaseIn);
    }

    void OnEnd()
    {
        Debug.Log("OnEnd");
    }
}
