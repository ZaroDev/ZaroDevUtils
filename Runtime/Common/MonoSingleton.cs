using UnityEngine;


namespace ZaroDev.Utils.Runtime.Common
{
    public class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this as T;
            DontDestroyOnLoad(this);
        }

    }
}