using System;
using System.Collections;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Utilities
{
    public static class CoroutinesExtensions
    {
        public static void StartCoroutineUniversalWait(this MonoBehaviour monoBehaviour, float waitTime, Action action)
        {
            monoBehaviour.StartCoroutine(UniversalWait(waitTime, action));
        }

        private static IEnumerator UniversalWait(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action();
        }
    }
}