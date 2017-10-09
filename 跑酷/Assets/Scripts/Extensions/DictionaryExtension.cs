using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DictionaryExtension  {


    /// <summary>
    /// 将键和值添加或替换到字典中：如果不存在，则添加；存在，则替换
    /// </summary>
    public static Dictionary<TKey, TValue> AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
    {
        if(dict.ContainsKey(key)==false)
        {
            dict.Add(key, value);
            return dict;
        }
        dict[key] = value;
        return dict;
    }
}
