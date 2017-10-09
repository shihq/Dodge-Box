using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 窗口内容 成员UIType（窗口名字以及预设体的路径）
/// </summary>
public class ContextBase  {

    /// <summary>
    /// UI类型
    /// </summary>
    public UIType MenuType { get; private set; }

    public ContextBase(UIType menuType)
    {
        MenuType = menuType;
    }
}
