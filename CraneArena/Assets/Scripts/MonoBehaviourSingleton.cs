using System;

using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));

                if (_instance == null)
                {
                    throw new InvalidOperationException($"No {nameof(T)} in current scene");
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = gameObject.GetComponent<T>();
        }
    }

    protected virtual void OnDestroy()
    {
        _instance = null;
    }
}
