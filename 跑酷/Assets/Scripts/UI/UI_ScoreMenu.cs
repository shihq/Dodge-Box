using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 得分菜单内容
/// </summary>
public class UI_ScoreMenuContext : ContextBase
{
    public UI_ScoreMenuContext(): base(UIType.UI_ScoreMenu)
    {

    }
}

public class UI_ScoreMenu:UIBase
{
    
    private Text[] score=new Text[3];
    private Text[] scoreTime=new Text[3];

    private Button returnButton;

    private CanvasGroup canvasGroup;

    private Tweener tweenerCanvas;
    

    private void Awake()
    {
        returnButton = Tool.FindChildGameObjectWithName(this.gameObject, "ReturnButton").GetComponent<Button>();
        returnButton.onClick.AddListener(()=>this.OnButtonClick());
        
        score[0] = Tool.FindChildGameObjectWithName(this.gameObject, "Score1Text").GetComponent<Text>();
        score[1] = Tool.FindChildGameObjectWithName(this.gameObject, "Score2Text").GetComponent<Text>();
        score[2] = Tool.FindChildGameObjectWithName(this.gameObject, "Score3Text").GetComponent<Text>();
        
        scoreTime[0] = Tool.FindChildGameObjectWithName(this.gameObject, "Score1Time").GetComponent<Text>();
        scoreTime[1]  = Tool.FindChildGameObjectWithName(this.gameObject, "Score2Time").GetComponent<Text>();
        scoreTime[2]  = Tool.FindChildGameObjectWithName(this.gameObject, "Score3Time").GetComponent<Text>();

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

        for (int i = 0; i < 3; i++)
        {
            if (GameManager.Instance().maxHistoryScore[i]!=0)
            {
                score[i].text = GameManager.Instance().maxHistoryScore[i].ToString();
                scoreTime[i].text = GameManager.Instance().maxhistoryDateTime[i].ToString();
            }
            else
            {
                score[i].text = "0";
                scoreTime[i].text = "None";
            }
        }
        
        
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
    
    
    public  void OnButtonClick()
    {
        MouseClick1();
        ContextManager.Instance().Pop();
               
        
    }
}