using UnityEngine;
using System.Collections;
using Tween.Curve;

namespace Tween
{
    public class TweenPosition : TweenObject
    {
        public void MoveTo(EasingType easingType, Vector2 startPosition, Vector2 endPosition, float duration, float delay = 0)
        {
            this.Easing = easingType;
            this.From = startPosition;
            this.MoveTo(endPosition, duration, delay);
        }

        public void MoveTo(Vector2 startPosition, Vector2 endPosition, float duration, float delay = 0)
        {
            this.From = startPosition;
            this.MoveTo(endPosition, duration, delay);
        }

        public void MoveTo(Vector2 endPosition, float duration, float delay = 0)
        {
            this.Duration = duration;
            this.From = this.transform.localPosition;
            this.To = endPosition;
            this.Delay = delay;

            this.MoveTo();
        }

        public void MoveTo()
        {
            this.Play();
        }

        public override void Play()
        {
            base.Play();

            StopCoroutine("Move");
            StartCoroutine("Move");
        }

        public override void Stop()
        {
            base.Stop();

            StopCoroutine("Move");
        }

        private IEnumerator Move()
        {
            yield return new WaitForSeconds(this.Delay);
            float timer = 0.0f;

            do
            {
                Vector3 pos = Vector3.Lerp(From, To, curve_.Evaluate(timer / this.Duration));

                if (base.IsPlay)
                {
                    timer += Time.deltaTime;

                    this.transform.localPosition = pos;

                    yield return null;

                    if (timer > this.Duration && this.Option == Option.PingPong)
                    {
                        Vector2 temp = From;
                        From = To;
                        To = temp;
                        timer = 0;
                    }
                }
                else
                {
                    yield return null;
                }
            } while (timer <= this.Duration);

            this.transform.localPosition = To;

            if (this.OnComplete != null)
            {
                this.OnComplete.Invoke();
            }
        }
    }
}