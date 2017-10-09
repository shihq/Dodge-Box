using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEditor;

/// <summary>
/// 存档管理器
/// </summary>
public class SaveManager
{
    public static readonly string extension = ".uml";

    /// <summary>
    /// 存储文件的名称
    /// </summary>
    public string saveDataFileName = "saveData";

    /// <summary>
    /// 序列化的类型
    /// </summary>
    public string[] serializedTypes;

    /// <summary>
    /// 存放需要的储存的数据类集合
    /// </summary>
    public DataContainer[] serializedData;
    
    /// <summary>
    /// data用于存放key(string)以及对应的value(Object)
    /// </summary>
    private Dictionary<string,System.Object> data=new Dictionary<string, object>();


    /// <summary>
    /// 根据key值获取对应的value，以及存储对应的value
    /// </summary>
    /// <param name="key"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public System.Object this[string key]
    {
        get { return data[key]; }

        set
        {
            if (typeof(Component).IsAssignableFrom(value.GetType()))
            {
                throw new System.InvalidOperationException("不能序列化");
            }

            data[key] = value;
        }
    }

    public SaveManager()
    {
        
    }

    public SaveManager(string fileName)
    {
        this.saveDataFileName = fileName;
    }


    /// <summary>
    /// Load导入XML文件
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static SaveManager Load(string path)
    {
        if (File.Exists(path)&&Path.GetExtension(path)==extension)
        {
            List<System.Type> additionalTypes=new List<Type>();
            XmlDocument document=new XmlDocument();
            document.Load(path);

            XmlNode objectNode = document.ChildNodes[1];

            foreach (XmlNode node in objectNode["serializedTypes"].ChildNodes)
            {
                additionalTypes.Add(System.Type.GetType(node.InnerXml));
            }
            
            XmlSerializer serializer=new XmlSerializer(typeof(SaveManager),additionalTypes.ToArray());
            
            TextReader textReader=new StreamReader(path);

            SaveManager instance = (SaveManager) serializer.Deserialize(textReader);
            textReader.Close();

            foreach (DataContainer container in instance.serializedData)
            {
                instance[container.key] = container.value;
            }

            instance.serializedData = null;

            return instance;

        }
        else
        {
            throw  new System.InvalidOperationException("没有该文件");
        }
    }

    public void Save()
    {
        Save(Application.persistentDataPath+"/"+saveDataFileName+extension);
    }
    
    /// <summary>
    /// Save保存XML文件
    /// </summary>
    /// <param name="path"></param>
    public void Save(string path)
    {
        List<System.Type> additionTypes=new List<Type>();
        List<string> typeNameList=new List<string>();
        List<DataContainer> dataList=new List<DataContainer>();

        System.Object result;
        System.Type resulType;

        //将dictionary集合转化为一个List
        foreach (string key in data.Keys)
        {
            result = data[key];
            resulType = result.GetType();

            if (!resulType.IsPrimitive&&!additionTypes.Contains(resulType))
            {
                additionTypes.Add(resulType);
                typeNameList.Add(resulType.AssemblyQualifiedName);
            }
            
            dataList.Add(new DataContainer(key,result));
        }

        //将list转化为数组
        serializedData = dataList.ToArray();
        serializedTypes = typeNameList.ToArray();
        
        XmlSerializer serializer=new XmlSerializer(typeof(SaveManager),additionTypes.ToArray());
        TextWriter textWriter=new StreamWriter(path);
        
        serializer.Serialize(textWriter,this);
        textWriter.Close();

        serializedData = null;
        serializedTypes = null;

    }


    /// <summary>
    /// 查看dictionary中是否有key对应的object
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool HasKey(string key)
    {
        return data.ContainsKey(key);
    }

    /// <summary>
    /// 返回data中key对应的成员
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetValue<T>(string key)
    {
        return (T) data[key];
    }


    /// <summary>
    /// 判断data中key值是否有Objcet存在以及是否对应T
    /// </summary>
    /// <param name="key"></param>
    /// <param name="result"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public bool TryGetValue<T>(string key, out T result)
    {
        System.Object resultOut;

        if (data.TryGetValue(key,out resultOut)&&resultOut.GetType()==typeof(T))
        {
            result = (T)resultOut;
            return true;
        }
        else
        {
            result = default(T);
            return false;
        }
    }
    

}

/// <summary>
/// 需要保存的类 key-value key 保存类型别名 value是需要保存的类
/// </summary>
public class DataContainer
{
    public string key;
    public System.Object value;

    public DataContainer()
    {
        
    }

    public DataContainer(string key, System.Object value)
    {
        this.key = key;
        this.value = value;
    }
}