using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

/// <summary>
/// 普通class单例模式
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> where T : Singleton<T> {
    protected static T _instance = null;

    protected Singleton() {
    }

    public static T Instance() {
        if (_instance == null) {
            _instance = (T)Activator.CreateInstance(typeof(T), true);


            return _instance;
        }

        return _instance;
    }
}