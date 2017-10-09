using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 定义了定义UI窗口基类 规定了UI窗口进入、退出、暂停、恢复、销毁的五个个方法
/// </summary>
public abstract class UIBase:MonoBehaviour
{
    /// <summary>
    /// 进入
    /// </summary>
    /// <param name="context"></param>
    public virtual void OnEnter(ContextBase context)
    {

    }

    /// <summary>
    /// 退出
    /// </summary>
    /// <param name="context"></param>
    public virtual void OnExit(ContextBase context)
    {

    }

    /// <summary>
    /// 暂停
    /// </summary>
    /// <param name="context"></param>
    public virtual void OnPause(ContextBase context)
    {

    }

    /// <summary>
    /// 恢复
    /// </summary>
    /// <param name="context"></param>
    public virtual void OnResume(ContextBase context)
    {

    }

    //销毁
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    

    //鼠标点击声音1
    public void MouseClick1()
    {
        AudioController.Instance().SoundPlay("Click1");
    }
    
    //鼠标点击声音2
    public void MouseClick2()
    {
        AudioController.Instance().SoundPlay("Click2");
    }
    
    //鼠标点击声音3
    public void MouseClick3()
    {
        AudioController.Instance().SoundPlay("Click3");
    }
}
