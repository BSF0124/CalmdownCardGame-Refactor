using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [DefaultExecutionOrder(-100)]
    public class CoreManager : MonoBehaviour
    {
        private static CoreManager _instance;
        public static CoreManager I => _instance;

        private readonly Dictionary<Type, object> _managers = new Dictionary<Type, object>();

        void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // 매니저 등록
        public void RegisterManager<T>(T manager) where T : class
        {
            var type = typeof(T);
            if (_managers.ContainsKey(type))
                Debug.LogWarning($"Manager {type.Name} is already registered.");
            else
                _managers[type] = manager;
        }

        // 등록된 매니저 조회
        public T GetManager<T>() where T : class
        {
            var type = typeof(T);
            if (_managers.TryGetValue(type, out var mgr))
                return mgr as T;

            Debug.LogError($"Manager {type.Name} not found.");
            return null;
        }
    }
}