using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI预设体存储信息
/// </summary>
public class UIType
{

    /// <summary>
    /// 预设体路径
    /// </summary>
    public string Path
    {
        get;
        private set;
    }

    /// <summary>
    /// 预设体名称
    /// </summary>
    public string Name
    {
        get;
        private set;
    }

    public UIType(string path)
    {
        Path = path;
        Name = path.Substring(path.LastIndexOf('/') + 1);
    }

    //返回UI窗口名称以及路径
    public string ToString()
    {
        return string.Format("Path : {0} Name= {1}", Path, Name);

    }

    public static readonly UIType UI_MainMenu = new UIType("Prefabs/UI/Menu/UI_MainMenu");
    public static readonly UIType UI_ScoreMenu = new UIType("Prefabs/UI/Menu/UI_ScoreMenu");
    public static readonly UIType UI_GamePanel = new UIType("Prefabs/UI/Menu/UI_GamePanel");
    public static readonly UIType UI_OptionMenu = new UIType("Prefabs/UI/Menu/UI_OptionMenu");
    public static readonly UIType UI_GameMenu = new UIType("Prefabs/UI/Menu/UI_GameMenu");
    public static readonly UIType UI_GameOverMenu = new UIType("Prefabs/UI/Menu/UI_GameOverMenu");


}

