using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// UIManager负责根据UIType管理窗口（创建、返回、销毁）
/// </summary>
public class UIManager : MonoSingleton<UIManager> {

    /// <summary>
    /// 存储当前UI界面
    /// </summary>
    [SerializeField]
    public Dictionary<UIType, GameObject> _UIDict = new Dictionary<UIType, GameObject>();

    private Transform canvas;



    private void Awake()
    {
        //销毁Canvas下所有的GameObject
        canvas = GameObject.FindWithTag("Canvas").transform;

        
        foreach(Transform item in canvas)
        {
            GameObject.Destroy(item.gameObject);
        }
        
    }

    
    /// <summary>
    /// 根据UIType返回窗口 如果没有则创建
    /// </summary>
    /// <param name="uiType"></param>
    /// <returns></returns>
    public GameObject GetSingleUI(UIType uiType)
    {
        //如果没有该窗口则创建
        if(_UIDict.ContainsKey(uiType)==false||_UIDict[uiType]==null)
        {
            GameObject go = GameObject.Instantiate(Resources.Load<GameObject>(uiType.Path)) as GameObject;

            go.name = uiType.Name;
            go.transform.SetParent(canvas,false);

            _UIDict.AddOrReplace(uiType, go);  

            return go;
        }

        return _UIDict[uiType];
    }

    /// <summary>
    /// 根据输入的UIType销毁窗口
    /// </summary>
    /// <param name="uiType"></param>
    public void Destroy(UIType uiType)
    {
        if(!_UIDict.ContainsKey(uiType))
        {
            return;
        }

        if(_UIDict[uiType]==null)
        {
            _UIDict.Remove(uiType);
            return;
        }

        GameObject.Destroy(_UIDict[uiType]);
        _UIDict.Remove(uiType);

        return;
    }
}
