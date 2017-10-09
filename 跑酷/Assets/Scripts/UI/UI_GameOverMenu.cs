using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 游戏结束菜单
/// </summary>
public class UI_GameOverMenuContext : ContextBase
{
    public UI_GameOverMenuContext(): base(UIType.UI_GameOverMenu)
    {

    }
}

public class UI_GameOverMenu:UIBase
{

    private Button restartButton;
    private Button mainButton;

    private RectTransform celebrateTextRectTransform;

    private Text scoreText;
    private Text celebrateText;
    


    private CanvasGroup canvasGroup;

    private Tweener tweenerCanvas;
    
    private Tweener tweenerText;
    

    private void Awake()
    {
        restartButton = Tool.FindChildGameObjectWithName(this.gameObject, "RestartButton").GetComponent<Button>();
        mainButton = Tool.FindChildGameObjectWithName(this.gameObject, "MainMenuButton").GetComponent<Button>();
        
        celebrateTextRectTransform = Tool.FindChildGameObjectWithName(this.gameObject, "CelebrateText").GetComponent<RectTransform>();
        celebrateText=Tool.FindChildGameObjectWithName(this.gameObject, "CelebrateText").GetComponent<Text>();
        
        scoreText=Tool.FindChildGameObjectWithName(this.gameObject, "ScoreText ").GetComponent<Text>();



        
        restartButton.onClick.AddListener(()=>this.OnButtonClick(restartButton.gameObject));
        mainButton.onClick.AddListener(()=>this.OnButtonClick(mainButton.gameObject));
        

        canvasGroup = GetComponent<CanvasGroup>();
        


        tweenerCanvas = canvasGroup.DOFade(0, 0.5f);
        tweenerCanvas.SetAutoKill(false);
        tweenerCanvas.Pause();

    }

    private void OnEnable()
    {

        int num = 0;
        bool isNewRecord = false;
        
        while (num<3)
        {
            if (GameManager.Instance().nowScore>GameManager.Instance().maxHistoryScore[num])
                break;
            num++;
        }


        if (num<3)
        {
            if (GameManager.Instance().maxHistoryScore[num]!=0)
            {
                for (int i = 2; i > num; i--)
                {
                    GameManager.Instance().maxHistoryScore[i] = GameManager.Instance().maxHistoryScore[i-1];
                    GameManager.Instance().maxhistoryDateTime[i] = GameManager.Instance().maxhistoryDateTime[i-1];
                }

                GameManager.Instance().maxHistoryScore[num] = GameManager.Instance().nowScore;
                GameManager.Instance().maxhistoryDateTime[num]=DateTime.Now;
                GameManager.Instance().Save();
                
            }
            else
            {
                GameManager.Instance().maxHistoryScore[num] = GameManager.Instance().nowScore;
                GameManager.Instance().maxhistoryDateTime[num]=DateTime.Now;
            }


            isNewRecord = true;
        }
        
        scoreText.text = GameManager.Instance().nowScore.ToString();

        if (isNewRecord)
        {
            tweenerText = celebrateTextRectTransform.DOScale(1.2f, 1.5f).SetLoops(-1,LoopType.Yoyo);
            celebrateText.text ="<color=#FFED00FF><size=40>congratulation ,you made a new recood!</size></color>";
            celebrateTextRectTransform.DOPlay();
            tweenerText.SetAutoKill(false); 
        }
        else
        {
            celebrateTextRectTransform.DOPause();
            celebrateText.text = "<color=#FFFFFFFF><size=20>play again,and you will make a new record</size></color>";

        }
        
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
            case "RestartButton":
                ContextManager.Instance().Pop();
                GameManager.Instance().RestartGame();
                GameManager.Instance().IsStartGame = true;
                break;
            case "MainMenuButton":
                ContextManager.Instance().Pop();
                ContextManager.Instance().Pop();
                GameManager.Instance().RestartGame();
                GameManager.Instance().IsStartGame = false;
                break;
            default:
                break;
               
        }
               
        
    }
}