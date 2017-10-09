using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehaiviour模式的单利模式
/// </summary>
public abstract class MonoSingleton<T> : MonoBehaviour where T:MonoSingleton<T>{

    private static T _instance = null;

    public static T Instance()
    {
        if(_instance==null)
        {
            _instance = FindObjectOfType<T>();

            if (FindObjectsOfType<T>().Length>1)
            {
                return _instance;
            }

            if(_instance==null)
            {
                string instanceName = "GameManager";
                GameObject instanceGameObject = GameObject.Find(instanceName);

                if(instanceGameObject==null)
                {
                    instanceGameObject = new GameObject(instanceName);
                }

                _instance = instanceGameObject.AddComponent<T>();

                DontDestroyOnLoad(instanceGameObject);
            }


        }

        return _instance;
    }

    public virtual void OnDestory()
    {

        _instance = null;
    }




}
