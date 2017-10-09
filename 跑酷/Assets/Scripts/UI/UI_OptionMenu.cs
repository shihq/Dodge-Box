using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 设置菜单内容
/// </summary>
public class UI_OptionMenuContext : ContextBase
{
    public UI_OptionMenuContext(): base(UIType.UI_OptionMenu)
    {

    }
}

public class UI_OptionMenu:UIBase
{

    private Slider BGMSlider;
    private Slider soundSlider;

    private Button returnButton;

    private CanvasGroup canvasGroup;

    private Tweener tweenerCanvas;

    private void Awake()
    {
        BGMSlider = Tool.FindChildGameObjectWithName(this.gameObject,"BGMSlider").GetComponent<Slider>();
        soundSlider = Tool.FindChildGameObjectWithName(this.gameObject,"SoundSlider").GetComponent<Slider>();

        returnButton = Tool.FindChildGameObjectWithName(this.gameObject, "ReturnButton").GetComponent<Button>();

        canvasGroup = GetComponent<CanvasGroup>();

        tweenerCanvas = canvasGroup.DOFade(0, 0.5f);
        tweenerCanvas.SetAutoKill(false);
        tweenerCanvas.Pause();

        BGMSlider.onValueChanged.AddListener(arg0 =>this.BGMSliderValueChanged() );
        soundSlider.onValueChanged.AddListener((arg0 =>this.SoundSliderValueChanged()));
        
        
        returnButton.onClick.AddListener(()=>this.OnButtonClick());

    }

    public override void OnEnter(ContextBase context)
    {
        this.gameObject.SetActive(true);

        BGMSlider.value = AudioController.Instance().BGMVolume;
        soundSlider.value = AudioController.Instance().SoundFXVolume;

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

    public void BGMSliderValueChanged()
    {
        AudioController.Instance().BGMVolume =BGMSlider.value;
    }
    
    public void SoundSliderValueChanged()
    {
        AudioController.Instance().SoundFXVolume = soundSlider.value;
    }

    
    public  void OnButtonClick()
    {
        MouseClick1();
        ContextManager.Instance().Pop();

    }
       
}