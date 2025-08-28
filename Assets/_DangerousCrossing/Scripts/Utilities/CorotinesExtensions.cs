using System;
using System.Collections;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Utilities
{
    public static class CoroutinesExtensions
    {
        public static void UniversalWait(this MonoBehaviour monoBehaviour, float waitTime, Action action)
        {
            monoBehaviour.StartCoroutine(_UniversalWait(waitTime, action));
        }

        private static IEnumerator _UniversalWait(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action();
        }
    }
}