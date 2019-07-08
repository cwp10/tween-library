/**
 * @class CoroutineHandler
 * @brief MonoBehaviour 상속이 아닌 클래스의 코루틴을 사용할때 사용
 */

using UnityEngine;
using System.Collections;

namespace Common
{
    public class CoroutineHandler : UnitySingleton<CoroutineHandler>
    {
        public static Coroutine StartStaticCoroutine(IEnumerator coroutine)
        {
            return Instance.StartCoroutine(coroutine);
        }

        public static void StopStaticCoroutine(Coroutine coroutine)
        {
            Instance.StopCoroutine(coroutine);
        }

        public static void StopStaticCoroutine(IEnumerator coroutine)
        {
            Instance.StopCoroutine(coroutine);
        }
    }
}