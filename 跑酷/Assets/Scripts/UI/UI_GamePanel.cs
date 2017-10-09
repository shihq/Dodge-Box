using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 游戏面板
/// </summary>
public class UI_GamePanelContext : ContextBase
{
    public UI_GamePanelContext(): base(UIType.UI_GamePanel)
    {

    }
}

public class UI_GamePanel:UIBase
{

    public Image bluePropPicture;
    public Image greenPropPicture;
    public Image yellowPropPicture;
    public Image redPropPicture;

    public Text bluePropText;
    public Text greenPropText;
    public Text yellowPropText;
    public Text redPropText;

    public Text scoreText;
    public Text addScoreText;

    public CanvasGroup canvasGroup;
    
    public Tweener tweenerCanvas;
    

    private void Awake()
    {
        bluePropPicture=Tool.FindChildGameObjectWithName(this.gameObject,"BluePropPicture").GetComponent<Image>();
        greenPropPicture=Tool.FindChildGameObjectWithName(this.gameObject,"GreenPropPicture").GetComponent<Image>();
        yellowPropPicture=Tool.FindChildGameObjectWithName(this.gameObject,"YellowPropPicture").GetComponent<Image>();
        redPropPicture=Tool.FindChildGameObjectWithName(this.gameObject,"RedPropPicture").GetComponent<Image>();
        
        bluePropText=Tool.FindChildGameObjectWithName(this.gameObject,"BluePropText").GetComponent<Text>();
        greenPropText=Tool.FindChildGameObjectWithName(this.gameObject,"GreenPropText").GetComponent<Text>();
        yellowPropText=Tool.FindChildGameObjectWithName(this.gameObject,"YellowPropText").GetComponent<Text>();
        redPropText=Tool.FindChildGameObjectWithName(this.gameObject,"RedPropText").GetComponent<Text>();
        
        
        scoreText=Tool.FindChildGameObjectWithName(this.gameObject,"ScoreText").GetComponent<Text>();
        addScoreText=Tool.FindChildGameObjectWithName(this.gameObject,"AddScoreText").GetComponent<Text>();
        
        canvasGroup = GetComponent<CanvasGroup>();

        tweenerCanvas = canvasGroup.DOFade(0, 0.5f);
        tweenerCanvas.SetAutoKill(false);
        tweenerCanvas.Pause();

    }

    private void Update()
    {
        if (GameManager.Instance().IsStartGame)
        {
            if (GameManager.Instance().isPause==false)
            {
                bluePropText.text = GameManager.Instance().bluePropNum.ToString();
                greenPropText.text = GameManager.Instance().greenPropNum.ToString();
                yellowPropText.text = GameManager.Instance().yellowPropNum.ToString();
                redPropText.text = GameManager.Instance().redPropNum.ToString();

                scoreText.text = GameManager.Instance().score.ToString();
                
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    GameManager.Instance().isPause = true;
                    ContextManager.Instance().Push(new UI_GameMenuContext());

                }

            }
        }


    }

    public override void OnEnter(ContextBase context) 
    {
        this.gameObject.SetActive(true);

        bluePropText.text = "0";
        greenPropText.text = "0";
        yellowPropText.text = "0";
        redPropText.text = "0";

        scoreText.text = "0";
        addScoreText.text = "";
        
        AudioController.Instance().BGMPlay("BackgroundMusic");
        canvasGroup.DOPlayBackwards();
    }


    public override void OnExit(ContextBase context)
    {
        canvasGroup.DOPlayForward();
        this.gameObject.SetActive(false);
    }

    public override void OnPause(ContextBase context)
    {

    }

    public override void OnResume(ContextBase context)
    {
        
    }

    public void InitializePanel()
    {
        scoreText.text = "0";
        bluePropText.text = "0";
        greenPropText.text = "0";
        redPropText.text = "0";
        yellowPropText.text = "0";
    }
    
    
}