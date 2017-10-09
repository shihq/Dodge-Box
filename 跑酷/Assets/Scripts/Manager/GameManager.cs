using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{

	#region 存档相关变量
	
	//存档管理器
	private SaveManager data;
	//存档名称
	private string saveDataFileName = "saveData";
	//是否存档
	private bool flagIsSave;
	//是否第一次进行游戏
	private bool isFirstPlay;
	
	//最高分数组
	public int[] maxHistoryScore;
	//最高分获得时间数组
	public DateTime[] maxhistoryDateTime;
	
	//是否播放音乐
	public bool isAudioPlay;
	//游戏音量
	public float bgmVolume;
	
	//是否播放音乐
	public bool isSoundFXPlay;
	//音效音量
	public float soundVolume;

	#endregion
	

	public SaveManager Data
	{
		get { return data; }
		set { data = value; }
	}

	#region 道具情况
	
	public int coinScore=100;
	
	public int redPropScore=40;
	public int yellowPropScore=40;
	public int greenPropScore=40;
	public int bluePropScore=40;
	

	#endregion
	//道具得分

	
	#region 玩家游戏时状态
	
	//是否开始游戏
	private bool isStartGame=false;
	
	//玩家是否死亡
	public bool isDead=false;
	
	//玩家是否暂停
	public bool isPause=false;
	
	//玩家当前层数
	public int nowLevel;

	//玩家分数
	public int score;
	
	//玩家此次分数
	public int nowScore;
	
	//拥有道具数量
	public int redPropNum=0;
	public int yellowPropNum=0;
	public int greenPropNum=0;
	public int bluePropNum=0;

	#endregion
	
	private CameraFellow cameraFellow;


	public bool IsStartGame
	{
		get { return isStartGame; }
		set
		{
			isStartGame = value;
			if (value)
			{
				StartGame();
			}
			else
			{
				//删除地图
				MapManager.Instance().DestoryAllMap();
			}
			
		}
	}


	private void Awake()
	{
		
		InitializeGameData();
		
		cameraFellow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFellow>();
		
		Data=new SaveManager();

		if (File.Exists(Application.persistentDataPath+"/"+saveDataFileName+".uml")==false)
		{
			isFirstPlay = true;
			flagIsSave = true;
			GenerateData();
		}
		
		Load();

		if (isFirstPlay&&!flagIsSave)
		{
			Load();
		}
		

		DontDestroyOnLoad(this.gameObject);

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void OnApplicationQuit()
	{
		Save();
	}

	/// <summary>
	/// 导入save数据
	/// </summary>
	public void Load()
	{
		data = SaveManager.Load(Application.persistentDataPath + "/" + saveDataFileName + ".uml");

		maxHistoryScore = Data.GetValue < int[] > ("HistoryScore");
		maxhistoryDateTime = Data.GetValue<DateTime[]>("HistoryScoreTime");
		isAudioPlay = Data.GetValue<bool>("isAudioPlay");
		bgmVolume = Data.GetValue<float>("bgmVolume");
		isSoundFXPlay=Data.GetValue<bool>("isSoundFXPlay");
		soundVolume=Data.GetValue<float>("soundVolume");
		
	}

	/// <summary>
	/// 存储save数据
	/// </summary>
	public void Save()
	{
		Data["HistoryScore"] = maxHistoryScore;
		Data["HistoryScoreTime"] = maxhistoryDateTime;
		Data["isAudioPlay"] = isAudioPlay;
		Data["bgmVolume"] = bgmVolume;
		Data["isSoundFXPlay"] = isSoundFXPlay;
		Data["soundVolume"] = soundVolume;
		Data.Save();
	}
	
	/// <summary>
	/// 重新开始游戏时 初始化游戏数据
	/// </summary>
	public void InitializeGameData()
	{
		isDead = false;
		isPause = false;
		
		score = 0;
		nowScore = 0;
		redPropNum = 0;
		yellowPropNum = 0;
		greenPropNum = 0;
		bluePropNum = 0;
	}

	/// <summary>
	/// 第一次游戏时 初始化保存游戏数据
	/// </summary>
	public void GenerateData()
	{
		maxHistoryScore=new int[3];
		maxhistoryDateTime=new DateTime[3];
		isAudioPlay = true;
		bgmVolume = 1f;
		isSoundFXPlay = true;
		soundVolume = 1f;
		
		GameManager.Instance().Data["HistoryScore"] = maxHistoryScore;
		GameManager.Instance().Data["HistoryScoreTime"] = maxhistoryDateTime;
		GameManager.Instance().Data["isAudioPlay"] = isAudioPlay;
		GameManager.Instance().Data["bgmVolume"] = bgmVolume;
		GameManager.Instance().Data["isSoundFXPlay"] = isSoundFXPlay;
		GameManager.Instance().Data["soundVolume"] = soundVolume;
		
		GameManager.Instance().Data.Save();	
	}

	/// <summary>
	/// 开始游戏(未完成)
	/// </summary>
	public void StartGame()
	{
		cameraFellow.transform.position=new Vector3(5.75f,4.5f,-10f);
		isDead = false;
		isPause = false;
		
		//初始化地图
		MapManager.Instance().InitializeMap();
		
		InitializeGameData();


	}

	/// <summary>
	/// 结束游戏（未完成）
	/// </summary>
	public void EndGame()
	{
		MapManager.Instance().DestoryAllMap();
		nowScore = score;
		
	}

	/// <summary>
	/// 重置游戏
	/// </summary>
	public void RestartGame()
	{
		cameraFellow.transform.position=new Vector3(5.75f,4.5f,-10f);

		IsStartGame = false;
		isDead = false;
		isPause = false;
		
	}
	

}
