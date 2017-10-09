using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 主菜单内容
/// </summary>
public class UI_MainMenuContext : ContextBase
{
    public UI_MainMenuContext(): base(UIType.UI_MainMenu)
    {

    }
}

public class UI_MainMenu:UIBase
{

    private Button startGameButton;

    private Button scoreButton;

    private Button optionButton;

    private Button exitButton;

    private RectTransform titleText;

    private CanvasGroup canvasGroup;

    private Tweener tweenerText;

    private Tweener tweenerCanvas;



    private void Awake()
    {
        startGameButton = Tool.FindChildGameObjectWithName(this.gameObject,"StartButton").GetComponent<Button>();
        scoreButton = Tool.FindChildGameObjectWithName(this.gameObject,"ScoreButton").GetComponent<Button>();
        optionButton = Tool.FindChildGameObjectWithName(this.gameObject,"OptionButton").GetComponent<Button>();
        exitButton = Tool.FindChildGameObjectWithName(this.gameObject,"ExitButton").GetComponent<Button>();
        titleText = Tool.FindChildGameObjectWithName(this.gameObject, "TitleText").GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        
        startGameButton.onClick.AddListener(()=>this.OnButtonClick(startGameButton.gameObject));
        scoreButton.onClick.AddListener(()=>this.OnButtonClick(scoreButton.gameObject));
        optionButton.onClick.AddListener(()=>this.OnButtonClick(optionButton.gameObject));
        exitButton.onClick.AddListener(()=>this.OnButtonClick(exitButton.gameObject));

        tweenerText = titleText.DOScale(1.2f, 1.5f).SetLoops(-1,LoopType.Yoyo);
        tweenerText.SetAutoKill(false);

        tweenerCanvas = canvasGroup.DOFade(0, 0.5f);
        tweenerCanvas.SetAutoKill(false);
        tweenerCanvas.Pause();


 
    }

    private void Update()
    {

    }

    public override void OnEnter(ContextBase context) 
    {
        Invoke("Play",0.5f);
    }


    public override void OnExit(ContextBase context)
    {
        base.OnExit(context);
        canvasGroup.DOPlayForward();
        this.gameObject.SetActive(false);
    }

    public override void OnPause(ContextBase context)
    {

        base.OnPause(context);
        canvasGroup.DOPlayForward();
        this.gameObject.SetActive(false);
    }

    public override void OnResume(ContextBase context)
    {
        
        Invoke("Play",0.5f);
        
        canvasGroup.DOPlayBackwards();
        this.gameObject.SetActive(true);
    }
    
    
    public  void OnButtonClick(GameObject obj)
    {
        MouseClick1();
        switch (obj.name)
        {
            case "StartButton":
                ContextManager.Instance().Push(new UI_GamePanelContext());
                GameManager.Instance().IsStartGame = true;
                break;
            case "ScoreButton":
                ContextManager.Instance().Push(new UI_ScoreMenuContext());
                break;
            case "OptionButton":
                ContextManager.Instance().Push(new UI_OptionMenuContext());
                break;
            case "ExitButton":
                Application.Quit();  
                break;
            default:
                break;
               
        }
    }

    public void Play()
    {
        if (AudioController.Instance().BGMAudioSource!=null)
        {
            if (AudioController.Instance().BGMAudioSource.clip.name=="BackgroundMusic")
            {
                AudioController.Instance().BGMPlay("MenuMusic");
            }
        }
        else
        {
            AudioController.Instance().BGMPlay("MenuMusic");
        }
    }

    
    
    /// <summary>  
    /// 播放背景音乐
    /// </summary>  
    /// <returns></returns>  
    private IEnumerator PlayMusic()
    {

        yield return new WaitForSeconds(0.5f);
        
        if (AudioController.Instance().BGMAudioSource!=null)
        {
            if (AudioController.Instance().BGMAudioSource.clip.name=="BackgroundMusic")
            {
                AudioController.Instance().BGMPlay("MenuMusic");
            }
        }
        else
        {
            AudioController.Instance().BGMPlay("MenuMusic");
        }


    }
}