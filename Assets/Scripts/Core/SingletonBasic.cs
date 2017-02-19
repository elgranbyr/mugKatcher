#undef SINGLETON_DEBUG
using UnityEngine;
using System;

public class SingletonBasic<T> : MonoBehaviour where T : SingletonBasic<T>
{
    // Singleton container
    public static GameObject Container
    {
        get;
        private set;
    }
    // init 
    private static bool initialized = false;
    // Singleton instance
    protected static T _instance = null;
    public static T Instance
    {
        get
        {
            // check if instance was initialized
            if (_instance == null)
            {


                string containerName = typeof(T).ToString() + "Container";
                

                if (Container != null)
                {                    
                    return _instance;
                }
                else
                {


                    // create new singleton
                    Container = new GameObject(containerName);
                    //Container.tag = containerName;
                    _instance = Container.AddComponent<T>();

                    // Problem during the creation, this should not happen
                    if (_instance == null)
                    {
                        Debug.LogError("Problem during the creation of " + typeof(T).ToString());
                    }
                }
                
                #if SINGLETON_DEBUG
                Debug.Log(msg);
                #endif
                if (!initialized)
                {
                    initialized = true;
                    _instance.Init();
                }
            }
            
            return _instance;
        }
    }
    

    /// <summary>
    /// Destroy Singleton when application ends
    /// </summary>
    public void OnApplicationQuit()
    {
        _instance = null;
        Container = null;
    }

    public void OnDestroy()
    {
        //Debug.Log("lo estoy matando");
        //_instance.OnDestroySingleton();
        if (OnDestroySingleton!=null)
        OnDestroySingleton();
        initialized = false;
        _instance = null;
        Container = null;
    }

    /// <summary>
    /// If no other monobehaviour request the instance in an awake function executing before this one,
    /// we don't need to search the object. 
    /// </summary>
    void Awake()
    {
        if (_instance == null)
        {
        #if SINGLETON_DEBUG
            Debug.Log("###############################[Singleton<" + typeof(T).ToString() + ">]: awakening null instance");
        #endif
            _instance = this as T;
            //_instance = SingletonBasic<T>.Instance;
            //_instance.Init();
        }
    }



    /// <summary>
    /// This function is called when the instance is used the first time
    /// Override this method and put all the initializations you need here, as you would do in Awake()
    /// </summary>
    protected virtual void Init() { }
    protected Action OnDestroySingleton;

}
