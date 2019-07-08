using System;
using UnityEngine;

namespace Common.Tween
{
    public class CurveGenerator
    {
        private delegate double Easing(double time, double min, double max, double duration);

        public static AnimationCurve Create(EasingType easingType)
        {
            AnimationCurve curve = new AnimationCurve();

            switch (easingType)
            {
                case EasingType.Linear:
                    curve = Calculate(curve, Linear, 2);
                    break;

                case EasingType.QuadEaseOut:
                    curve = Calculate(curve, QuadEaseOut, 15);
                    break;
                case EasingType.QuadEaseIn:
                    curve = Calculate(curve, QuadEaseIn, 15);
                    break;
                case EasingType.QuadEaseInOut:
                    curve = Calculate(curve, QuadEaseInOut, 15);
                    break;
                case EasingType.QuadEaseOutIn:
                    curve = Calculate(curve, QuadEaseOutIn, 15);
                    break;

                case EasingType.ExpoEaseOut:
                    curve = Calculate(curve, ExpoEaseOut, 15);
                    break;
                case EasingType.ExpoEaseIn:
                    curve = Calculate(curve, ExpoEaseIn, 15);
                    break;
                case EasingType.ExpoEaseInOut:
                    curve = Calculate(curve, ExpoEaseInOut, 15);
                    break;
                case EasingType.ExpoEaseOutIn:
                    curve = Calculate(curve, ExpoEaseOutIn, 15);
                    break;

                case EasingType.CubicEaseOut:
                    curve = Calculate(curve, CubicEaseOut, 15);
                    break;
                case EasingType.CubicEaseIn:
                    curve = Calculate(curve, CubicEaseIn, 15);
                    break;
                case EasingType.CubicEaseInOut:
                    curve = Calculate(curve, CubicEaseInOut, 15);
                    break;
                case EasingType.CubicEaseOutIn:
                    curve = Calculate(curve, CubicEaseOutIn, 15);
                    break;

                case EasingType.QuartEaseOut:
                    curve = Calculate(curve, QuartEaseOut, 15);
                    break;
                case EasingType.QuartEaseIn:
                    curve = Calculate(curve, QuartEaseIn, 15);
                    break;
                case EasingType.QuartEaseInOut:
                    curve = Calculate(curve, QuartEaseInOut, 15);
                    break;
                case EasingType.QuartEaseOutIn:
                    curve = Calculate(curve, QuartEaseOutIn, 15);
                    break;

                case EasingType.QuintEaseOut:
                    curve = Calculate(curve, QuintEaseOut, 15);
                    break;
                case EasingType.QuintEaseIn:
                    curve = Calculate(curve, QuintEaseIn, 15);
                    break;
                case EasingType.QuintEaseInOut:
                    curve = Calculate(curve, QuintEaseInOut, 15);
                    break;
                case EasingType.QuintEaseOutIn:
                    curve = Calculate(curve, QuintEaseOutIn, 15);
                    break;

                case EasingType.CircEaseOut:
                    curve = Calculate(curve, CircEaseOut, 15);
                    break;
                case EasingType.CircEaseIn:
                    curve = Calculate(curve, CircEaseIn, 15);
                    break;
                case EasingType.CircEaseInOut:
                    curve = Calculate(curve, CircEaseInOut, 15);
                    break;
                case EasingType.CircEaseOutIn:
                    curve = Calculate(curve, CircEaseOutIn, 15);
                    break;

                case EasingType.SineEaseOut:
                    curve = Calculate(curve, SineEaseOut, 15);
                    break;
                case EasingType.SineEaseIn:
                    curve = Calculate(curve, SineEaseIn, 15);
                    break;
                case EasingType.SineEaseInOut:
                    curve = Calculate(curve, SineEaseInOut, 15);
                    break;
                case EasingType.SineEaseOutIn:
                    curve = Calculate(curve, SineEaseOutIn, 15);
                    break;

                case EasingType.ElasticEaseOut:
                    curve = Calculate(curve, ElasticEaseOut, 30);
                    break;
                case EasingType.ElasticEaseIn:
                    curve = Calculate(curve, ElasticEaseIn, 30);
                    break;
                case EasingType.ElasticEaseInOut:
                    curve = Calculate(curve, ElasticEaseInOut, 30);
                    break;
                case EasingType.ElasticEaseOutIn:
                    curve = Calculate(curve, ElasticEaseOutIn, 30);
                    break;

                case EasingType.BounceEaseOut:
                    curve = Calculate(curve, BounceEaseOut, 30);
                    break;
                case EasingType.BounceEaseIn:
                    curve = Calculate(curve, BounceEaseIn, 30);
                    break;
                case EasingType.BounceEaseInOut:
                    curve = Calculate(curve, BounceEaseInOut, 30);
                    break;
                case EasingType.BounceEaseOutIn:
                    curve = Calculate(curve, BounceEaseOutIn, 30);
                    break;

                case EasingType.BackEaseOut:
                    curve = Calculate(curve, BackEaseOut, 30);
                    break;
                case EasingType.BackEaseIn:
                    curve = Calculate(curve, BackEaseIn, 30);
                    break;
                case EasingType.BackEaseInOut:
                    curve = Calculate(curve, BackEaseInOut, 30);
                    break;
                case EasingType.BackEaseOutIn:
                    curve = Calculate(curve, BackEaseOutIn, 30);
                    break;
            }

            return curve;
        }

        private static AnimationCurve Calculate(AnimationCurve curve, Easing easing, int resolution)
        {
            for (int i = 0; i < resolution; ++i)
            {
                float time = i / (resolution - 1f);
                float value = (float)easing(time, 0.0, 1.0, 1.0);
                Keyframe key = new Keyframe(time, value);
                curve.AddKey(key);
            }
            for (int i = 0; i < resolution; ++i)
            {
                curve.SmoothTangents(i, 0f);
            }
            return curve;
        }

        private static double Linear(double t, double b, double c, double d)
        {
            return c * t / d + b;
        }

        private static double ExpoEaseOut(double t, double b, double c, double d)
        {
            return (t == d) ? b + c : c * (-Math.Pow(2, -10 * t / d) + 1) + b;
        }

        private static double ExpoEaseIn(double t, double b, double c, double d)
        {
            return (t == 0) ? b : c * Math.Pow(2, 10 * (t / d - 1)) + b;
        }

        private static double ExpoEaseInOut(double t, double b, double c, double d)
        {
            if (t == 0)
                return b;

            if (t == d)
                return b + c;

            if ((t /= d / 2) < 1)
                return c / 2 * Math.Pow(2, 10 * (t - 1)) + b;

            return c / 2 * (-Math.Pow(2, -10 * --t) + 2) + b;
        }

        private static double ExpoEaseOutIn(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return ExpoEaseOut(t * 2, b, c / 2, d);

            return ExpoEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        private static double CircEaseOut(double t, double b, double c, double d)
        {
            return c * Math.Sqrt(1 - (t = t / d - 1) * t) + b;
        }

        private static double CircEaseIn(double t, double b, double c, double d)
        {
            return -c * (Math.Sqrt(1 - (t /= d) * t) - 1) + b;
        }

        private static double CircEaseInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1)
                return -c / 2 * (Math.Sqrt(1 - t * t) - 1) + b;

            return c / 2 * (Math.Sqrt(1 - (t -= 2) * t) + 1) + b;
        }

        private static double CircEaseOutIn(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return CircEaseOut(t * 2, b, c / 2, d);

            return CircEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        private static double QuadEaseOut(double t, double b, double c, double d)
        {
            return -c * (t /= d) * (t - 2) + b;
        }

        private static double QuadEaseIn(double t, double b, double c, double d)
        {
            return c * (t /= d) * t + b;
        }

        private static double QuadEaseInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * t * t + b;

            return -c / 2 * ((--t) * (t - 2) - 1) + b;
        }

        private static double QuadEaseOutIn(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return QuadEaseOut(t * 2, b, c / 2, d);

            return QuadEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        private static double SineEaseOut(double t, double b, double c, double d)
        {
            return c * Math.Sin(t / d * (Math.PI / 2)) + b;
        }

        private static double SineEaseIn(double t, double b, double c, double d)
        {
            return -c * Math.Cos(t / d * (Math.PI / 2)) + c + b;
        }

        private static double SineEaseInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * (Math.Sin(Math.PI * t / 2)) + b;

            return -c / 2 * (Math.Cos(Math.PI * --t / 2) - 2) + b;
        }

        private static double SineEaseOutIn(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return SineEaseOut(t * 2, b, c / 2, d);

            return SineEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        private static double CubicEaseOut(double t, double b, double c, double d)
        {
            return c * ((t = t / d - 1) * t * t + 1) + b;
        }

        private static double CubicEaseIn(double t, double b, double c, double d)
        {
            return c * (t /= d) * t * t + b;
        }

        private static double CubicEaseInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * t * t * t + b;

            return c / 2 * ((t -= 2) * t * t + 2) + b;
        }

        private static double CubicEaseOutIn(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return CubicEaseOut(t * 2, b, c / 2, d);

            return CubicEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        private static double QuartEaseOut(double t, double b, double c, double d)
        {
            return -c * ((t = t / d - 1) * t * t * t - 1) + b;
        }

        private static double QuartEaseIn(double t, double b, double c, double d)
        {
            return c * (t /= d) * t * t * t + b;
        }

        private static double QuartEaseInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * t * t * t * t + b;

            return -c / 2 * ((t -= 2) * t * t * t - 2) + b;
        }

        private static double QuartEaseOutIn(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return QuartEaseOut(t * 2, b, c / 2, d);

            return QuartEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        private static double QuintEaseOut(double t, double b, double c, double d)
        {
            return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
        }

        private static double QuintEaseIn(double t, double b, double c, double d)
        {
            return c * (t /= d) * t * t * t * t + b;
        }

        private static double QuintEaseInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * t * t * t * t * t + b;
            return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
        }

        private static double QuintEaseOutIn(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return QuintEaseOut(t * 2, b, c / 2, d);
            return QuintEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        private static double ElasticEaseOut(double t, double b, double c, double d)
        {
            if ((t /= d) == 1)
                return b + c;

            double p = d * .3;
            double s = p / 4;

            return (c * Math.Pow(2, -10 * t) * Math.Sin((t * d - s) * (2 * Math.PI) / p) + c + b);
        }

        private static double ElasticEaseIn(double t, double b, double c, double d)
        {
            if ((t /= d) == 1)
                return b + c;

            double p = d * .3;
            double s = p / 4;

            return -(c * Math.Pow(2, 10 * (t -= 1)) * Math.Sin((t * d - s) * (2 * Math.PI) / p)) + b;
        }

        private static double ElasticEaseInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) == 2)
                return b + c;

            double p = d * (.3 * 1.5);
            double s = p / 4;

            if (t < 1)
                return -.5 * (c * Math.Pow(2, 10 * (t -= 1)) * Math.Sin((t * d - s) * (2 * Math.PI) / p)) + b;
            return c * Math.Pow(2, -10 * (t -= 1)) * Math.Sin((t * d - s) * (2 * Math.PI) / p) * .5 + c + b;
        }

        private static double ElasticEaseOutIn(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return ElasticEaseOut(t * 2, b, c / 2, d);
            return ElasticEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        private static double BounceEaseOut(double t, double b, double c, double d)
        {
            if ((t /= d) < (1 / 2.75))
                return c * (7.5625 * t * t) + b;
            else if (t < (2 / 2.75))
                return c * (7.5625 * (t -= (1.5 / 2.75)) * t + .75) + b;
            else if (t < (2.5 / 2.75))
                return c * (7.5625 * (t -= (2.25 / 2.75)) * t + .9375) + b;
            else
                return c * (7.5625 * (t -= (2.625 / 2.75)) * t + .984375) + b;
        }

        private static double BounceEaseIn(double t, double b, double c, double d)
        {
            return c - BounceEaseOut(d - t, 0, c, d) + b;
        }

        private static double BounceEaseInOut(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return BounceEaseIn(t * 2, 0, c, d) * .5 + b;
            else
                return BounceEaseOut(t * 2 - d, 0, c, d) * .5 + c * .5 + b;
        }

        private static double BounceEaseOutIn(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return BounceEaseOut(t * 2, b, c / 2, d);
            return BounceEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        private static double BackEaseOut(double t, double b, double c, double d)
        {
            return c * ((t = t / d - 1) * t * ((1.70158 + 1) * t + 1.70158) + 1) + b;
        }

        private static double BackEaseIn(double t, double b, double c, double d)
        {
            return c * (t /= d) * t * ((1.70158 + 1) * t - 1.70158) + b;
        }

        private static double BackEaseInOut(double t, double b, double c, double d)
        {
            double s = 1.70158;
            if ((t /= d / 2) < 1)
                return c / 2 * (t * t * (((s *= (1.525)) + 1) * t - s)) + b;
            return c / 2 * ((t -= 2) * t * (((s *= (1.525)) + 1) * t + s) + 2) + b;
        }

        private static double BackEaseOutIn(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return BackEaseOut(t * 2, b, c / 2, d);
            return BackEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }
    }
}
