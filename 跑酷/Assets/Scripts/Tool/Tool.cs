using UnityEngine;
using System;

/// <summary>
/// 工具类
/// </summary>
public class Tool
{
    private static Tool instance;

    public static Tool Instance
    {
        get
        {
            if (instance==null)
            {
                instance=new Tool();
            }
            return instance;
        }
    }

    public static  GameObject FindChildGameObjectWithName(GameObject obj,string name)
    {
        if (obj.name==name)
        {
            return obj;
        }

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            GameObject child = obj.transform.GetChild(i).gameObject;

            var result=FindChildGameObjectWithName(child, name);

            if (result!=null)
            {
                return result;
            }

        }


        return null;
    }
    
    
}