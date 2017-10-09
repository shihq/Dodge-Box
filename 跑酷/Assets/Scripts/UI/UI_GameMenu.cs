using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 游戏暂停菜单
/// </summary>
public class UI_GameMenuContext : ContextBase
{
    public UI_GameMenuContext(): base(UIType.UI_GameMenu)
    {

    }
}

public class UI_GameMenu:UIBase
{

    private Button returnButton;
    private Button restartButton;
    private Button mainButton;
    


    private CanvasGroup canvasGroup;

    private Tweener tweenerCanvas;
    

    private void Awake()
    {
        returnButton = Tool.FindChildGameObjectWithName(this.gameObject, "ReturnButton").GetComponent<Button>();
        mainButton = Tool.FindChildGameObjectWithName(this.gameObject, "MainMenuButton").GetComponent<Button>();
        
        returnButton.onClick.AddListener(()=>this.OnButtonClick(returnButton.gameObject));
        restartButton.onClick.AddListener(()=>this.OnButtonClick(restartButton.gameObject));
        mainButton.onClick.AddListener(()=>this.OnButtonClick(mainButton.gameObject));
        

        canvasGroup = GetComponent<CanvasGroup>();

        tweenerCanvas = canvasGroup.DOFade(0, 0.5f);
        tweenerCanvas.SetAutoKill(false);
        tweenerCanvas.Pause();

    }

    private void Update()
    {

    }

    public override void OnEnter(ContextBase context) 
    {
        this.gameObject.SetActive(true);        
        canvasGroup.DOPlayBackwards();
    }


    public override void OnExit(ContextBase context)
    {
        canvasGroup.DOPlayForward();
        this.gameObject.SetActive(false);
    }

    public override void OnPause(ContextBase context)
    {

        canvasGroup.DOPlayForward();
        this.gameObject.SetActive(false);
    }

    public override void OnResume(ContextBase context)
    {
        this.OnEnter(context);
    }
    
    
    public  void OnButtonClick(GameObject obj)
    {
        MouseClick1();
        switch (obj.name)
        {
            case "ReturnButton":
                ContextManager.Instance().Pop();
                GameManager.Instance().isPause = false;
                break;
            case "MainMenuButton":
                ContextManager.Instance().Pop();
                ContextManager.Instance().Pop();
                GameManager.Instance().RestartGame();
                break;
            default:
                break;
               
        }       
        
    }
    
    
}