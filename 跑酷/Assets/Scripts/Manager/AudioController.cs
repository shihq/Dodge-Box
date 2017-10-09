using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioController : MonoSingleton<AudioController>
{
    private Dictionary<string,int> AudioDictionary=new Dictionary<string, int>();
    private const int MaxAudioCount = 20;

    public AudioSource BGMAudioSource;
    public AudioSource LastAudioSource;

    /// <summary>
    /// BGM音量
    /// </summary>
    public float BGMVolume
    {
        get { return GameManager.Instance().bgmVolume; }
        set
        {
            if (value>1f)
            {
                value = 1f;
            }
            else if (value<0f)
            {
                value = 0;
            }
            
            GameManager.Instance().bgmVolume = value;
            if (BGMAudioSource!=null)
            {
                BGMAudioSource.volume = value;
            }
        }
    }
    
    /// <summary>
    /// 游戏音效音量
    /// </summary>
    public float SoundFXVolume
    {
        get { return GameManager.Instance().soundVolume; }
        set
        {
            if (value>1f)
            {
                value = 1f;
            }
            else if (value<0f)
            {
                value = 0;
            }
            
            GameManager.Instance().soundVolume = value;
            if (LastAudioSource!=null)
            {
                LastAudioSource.volume = value;
            }
        }
    }

    

    private void Start()
    {
        
    }



    /// <summary>
    /// 播放音效（创建音效gameObject，并在结束播放时摧毁）
    /// </summary>
    /// <param name="name"></param>
    public void SoundPlay(string name)
    {

        if (!GameManager.Instance().isSoundFXPlay)
        {
            return;
        }
        if (AudioDictionary.ContainsKey(name))
        {
            if (AudioDictionary[name]<=MaxAudioCount)
            {
                AudioClip sound = GetAudioClip(name);
                if (sound!=null)
                {
                    StartCoroutine(PlayClipEnd(sound,name));
                    PlayClip(sound,SoundFXVolume,name);
                    AudioDictionary[name]++;
                }
            }
        }
        else
        {
            AudioDictionary.Add(name,1);
            AudioClip sound = GetAudioClip(name);
            if (sound!=null)
            {
                StartCoroutine(PlayClipEnd(sound,name));
                PlayClip(sound,SoundFXVolume,name);
                AudioDictionary[name]++;
            }
        }
        
    }
    
    
    /// <summary>  
    /// 根据音效名字暂停音效
    /// </summary>  
    /// <param name="audioname"></param>  
    public void SoundPause(string audioname)
    {
        if (LastAudioSource != null)
        {
            LastAudioSource.Pause();
        }
    }
    /// <summary>  
    /// 暂停所有音效音乐  
    /// </summary>  
    public void SoundAllPause()
    {
        AudioSource[] allsource = FindObjectsOfType<AudioSource>();
        if (allsource != null && allsource.Length > 0)
        {
            for (int i = 0; i < allsource.Length; i++)
            {
                allsource[i].Pause();
            }
        }
    }
    /// <summary>  
    /// 根据音效名字特定的音效  
    /// </summary>  
    /// <param name="audioname"></param>  
    public void SoundStop(string audioname)
    {
        GameObject obj = transform.Find(audioname).gameObject;
        if (obj != null)
        {
            Destroy(obj);
        }
    }

    /// <summary>
    /// 根据背景音乐名字播放背景音乐
    /// </summary>
    /// <param name="BGMName"></param>
    public void BGMPlay(string BGMName)
    {
        BGMStop();
        if (!GameManager.Instance().isAudioPlay)
        {
            return;
        }

        if (BGMName!=null)
        {
            AudioClip bgmMusic = GetAudioClip(BGMName);
            if (bgmMusic!=null)
            {
                this.PlayLoopBGMAudioClip(bgmMusic,BGMVolume);
            }
        }
    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void BGMPause()
    {
        if (BGMAudioSource!=null)
        {
            BGMAudioSource.Pause();   
        }
    }


    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void BGMStop()
    {
        if (BGMAudioSource!=null&&this.BGMAudioSource.gameObject)
        {
            Destroy(this.BGMAudioSource.gameObject);
            BGMAudioSource = null;
        }
    }

    /// <summary>
    /// 重新播放背景音乐
    /// </summary>
    public void BGMReplay()
    {
        if (BGMAudioSource!=null)
        {
            BGMAudioSource.Play();
        }
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="volume"></param>
    /// <param name="isloop"></param>
    /// <param name="name"></param>
    void PlayBGMAudioClip(AudioClip audioClip, float volume = 1f, bool isloop = true, string name = null)
    {
        if (audioClip == null)
        {
            return;
        }
        else
        {
            GameObject obj = new GameObject(name);
            obj.transform.parent = this.transform;
            AudioSource LoopClip = obj.AddComponent<AudioSource>();
            BGMAudioSource = LoopClip;
            BGMAudioSource.clip = audioClip;
            BGMAudioSource.volume = volume;
            BGMAudioSource.loop = true;
            BGMAudioSource.pitch = 1f;
            BGMAudioSource.Play();
        }
    }

    
    /// <summary>  
    /// 播放一次的背景音乐  
    /// </summary>  
    /// <param name="audioClip"></param>  
    /// <param name="volume"></param>  
    /// <param name="name"></param>  
    private void PlayOnceBGMAudioClip(AudioClip audioClip, float volume = 1f, string name = null)
    {
        PlayBGMAudioClip(audioClip, volume, false, name == null ? "BGMSound" : name);
    }
    
    /// <summary>  
    /// 循环播放的背景音乐  
    /// </summary>  
    /// <param name="audioClip"></param>  
    /// <param name="volume"></param>  
    /// <param name="audioName"></param>  
    private void PlayLoopBGMAudioClip(AudioClip audioClip, float volume = 1f, string audioName = null)
    {
        PlayBGMAudioClip(audioClip, volume, true, audioName == null ? "LoopSound" : audioName);
    }

    /// <summary>
    /// 播放游戏音效
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="volume"></param>
    /// <param name="musicName"></param>
    private void PlayClip(AudioClip audioClip, float volume =1f, string musicName=null)
    {
        if (audioClip==null)
        {
            return;
        }
        else
        {
            GameObject obj=new GameObject(musicName==null?"SoundClip":musicName);
            obj.transform.parent = this.transform;
            AudioSource source = obj.AddComponent<AudioSource>();
            StartCoroutine(this.PlayClipEndDestroy(audioClip,obj));
            source.pitch = 1f;
            source.volume = volume;
            source.clip = audioClip;
            source.Play();
            this.LastAudioSource = source;
        }
        
       
    }
    
    /// <summary>  
    /// 播放游戏音效后面删除物体  
    /// </summary>  
    /// <param name="audioclip"></param>  
    /// <param name="soundobj"></param>  
    /// <returns></returns>  
    private IEnumerator PlayClipEndDestroy(AudioClip audioclip, GameObject soundobj)
    {
        if (soundobj == null || audioclip == null)
        {
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(audioclip.length * Time.timeScale);
            Destroy(soundobj);
        }
    }
    
    /// <summary>  
    /// 播放音效后结束
    /// </summary>  
    /// <returns></returns>  
    private IEnumerator PlayClipEnd(AudioClip audioclip, string audioname)
    {
        if (audioclip != null)
        {
            yield return new WaitForSeconds(audioclip.length * Time.timeScale);
            AudioDictionary[audioname]--;
            if (AudioDictionary[audioname] <= 0)
            {
                AudioDictionary.Remove(audioname);
            }
        }
        yield break;
    }


    /// <summary>
    /// 从Resource加载audio
    /// </summary>
    /// <param name="audioName"></param>
    /// <returns></returns>
    public AudioClip GetAudioClip(string audioName)
    {
        AudioClip music = Resources.Load<AudioClip>("Music/" + audioName);
        return music;
    }
    
    
}