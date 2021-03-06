using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : JMC
{
    protected static Prototype1.UIManager _UI { get { return Prototype1.UIManager.INSTANCE; } }
    protected static Prototype2.UIManager _UI2 { get { return Prototype2.UIManager.INSTANCE; } }

    protected static Prototype3.UIManager _UI3 { get { return Prototype3.UIManager.INSTANCE; } }
    protected static UIManager4 _UI4 { get { return UIManager4.INSTANCE; } }
    protected static SceneLoader _SL { get { return SceneLoader.INSTANCE; } }
    protected static TempSave _TS { get { return TempSave.INSTANCE; } }
}

public class GameBehaviour<T> : GameBehaviour where T : GameBehaviour
{
    private static T instance_;
    public static T INSTANCE
    {
        get
        {
            if (instance_ == null)
            {
                instance_ = GameObject.FindObjectOfType<T>();
                if (instance_ == null)
                {
                    GameObject singleton = new GameObject(typeof(T).Name);
                    singleton.AddComponent<T>();
                }
            }
            return instance_;
        }
    }
    protected virtual void Awake()
    {
        if (instance_ == null)
        {
            instance_ = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
