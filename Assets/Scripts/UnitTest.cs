using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tween.Curve;

public class UnitTest : MonoBehaviour
{
    public AnimationCurve curve = null;

    private void Awake()
    {
        curve = CurveGenerator.Create(CurveType.BounceEaseInOut);
    }
}
