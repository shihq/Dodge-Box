using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


/// <summary>
/// ContextManager 窗口管理器 负责管理窗口变化的动画、以及数据的存储
/// </summary>
public class ContextManager:MonoSingleton<ContextManager> {

    public Stack<ContextBase> _contextStack = new Stack<ContextBase>();


    private void Awake()
    {

        //将主菜单压入栈
        Push(new  UI_MainMenuContext());
    }

    //进栈
    public void Push(ContextBase nextContext)
    {
        //如果不为空
        if(_contextStack.Count!=0)
        {
            ContextBase curContext = _contextStack.Peek();
            UIBase curUIBase = UIManager.Instance().GetSingleUI(curContext.MenuType).GetComponent<UIBase>();
            curUIBase.OnPause(curContext);
        }


        _contextStack.Push(nextContext);
        UIBase nextUIBase = UIManager.Instance().GetSingleUI(nextContext.MenuType).GetComponent<UIBase>();
        nextUIBase.OnEnter(nextContext);

    }

    //出栈
    public void Pop()
    {
        if (_contextStack.Count != 0)
        {
            ContextBase curContext = _contextStack.Peek();
            _contextStack.Pop();

            UIBase curUIBase = UIManager.Instance().GetSingleUI(curContext.MenuType).GetComponent<UIBase>();
            curUIBase.OnExit(curContext);
        }

        if (_contextStack.Count != 0)
        {
            ContextBase lastContext = _contextStack.Peek();
            UIBase curUIBase = UIManager.Instance().GetSingleUI(lastContext.MenuType).GetComponent<UIBase>();
            curUIBase.OnResume(lastContext);
        }
    }


}
