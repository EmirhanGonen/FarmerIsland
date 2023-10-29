using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : class
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        Instance = this as T;
    }
}

public abstract class DontDestroySingleton<T> : Singleton<T> where T : class
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
}