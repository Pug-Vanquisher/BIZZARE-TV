using System.Collections;
using UnityEngine;

namespace Balance
{
    public sealed class Coroutines : MonoBehaviour
    {
        private static Coroutines _instance;

        private static Coroutines instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
        }

        public static Coroutine StartRoutine(IEnumerator enumerator)
        {
            return instance.StartCoroutine(enumerator);
        }

        public static void StopRoutine(Coroutine routine)
        {
            instance.StopCoroutine(routine);
        }

        public static void StopAllRoutines()
        {
            instance.StopAllCoroutines();
        }

        public static void Destroy()
        {
            if (instance != null)
                Destroy(instance.gameObject);
        }
    }
}
