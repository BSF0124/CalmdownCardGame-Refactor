using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<T>();
                if (instance == null)
                {
                    Debug.LogError($"MonoSingleton<{typeof(T)}> instance not found in scene.");
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null) instance = this as T;
        else if (instance != this) Destroy(gameObject);
    }
}