using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lapis.Utilities
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T instance;

        private static bool isInitialized = false;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject($"${typeof(T)}$", typeof(T)).GetComponent<T>();
                    instance.Init();
                }

                return instance;
            }
        }

        public virtual void Init()
        {
            if (isInitialized)
                return;

            isInitialized = true;
            Debug.Log($"{typeof(T)} initialized.");
        }
    }
}
