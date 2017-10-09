using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地图生成器
/// </summary>
public class MapManager :MonoSingleton<MapManager>{

	private GameObject[] boxPrefabs=new GameObject[4];
	private GameObject wallPrefab;
	private GameObject player;
	private GameObject coin;
	private GameObject redProp;
	private GameObject greenProp;
	private GameObject blueProp;
	private GameObject yellowProp;

	private CameraFellow cameraFellow;
	
	public Transform map;

	private int colNum=10;
	private int rowNum=10;

	private int maxRowNum;
	private int minRowNum;

	public int nowRow;

	private Player playerGameObjecct;
	
	/// <summary>
	/// 地图dictionary列表
	/// </summary>
	public Dictionary<int, List<GameObject>> allObjects=new Dictionary<int, List<GameObject>>();



	private void Awake()
	{	
		//初始化预设体
		InitializePrefabs();
		//获取摄像机
		cameraFellow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFellow>();

		map = GameObject.Find("Map").transform;

	}


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		if (GameManager.Instance().IsStartGame==false)
		{
			return;
		}
		

		if (!GameManager.Instance().isDead&&!GameManager.Instance().isPause&&playerGameObjecct!=null)
		{
			if ((Mathf.Ceil(playerGameObjecct.transform.position.y)+10)>maxRowNum)
			{
				maxRowNum = (int)Mathf.Ceil(playerGameObjecct.transform.position.y)+10;
				if (maxRowNum<50)
				{
					ConstructLowEnvironment(maxRowNum);
				}
				else
				{
					ConstructHighEnvironment(maxRowNum);
				}


			}


			if (Mathf.Floor(playerGameObjecct.transform.position.y)>minRowNum)
			{
				DestroyEnvironment(minRowNum);
				minRowNum++;
			}
	
		}

	}

	/// <summary>
	/// 初始化地图
	/// </summary>
	public void InitializeMap()
	{
		//初始化地图
		for (int i = 0; i < rowNum+1; i++)
		{
			ConstructLowEnvironment(i);
		}
		
		maxRowNum = 10;
		minRowNum = 0;

		playerGameObjecct = GameObject.FindWithTag("Player").GetComponent<Player>();
		
		cameraFellow.FindPlayer();
	}

	/// <summary>
	/// 创建行数较低的每行地图
	/// </summary>
	/// <param name="num"></param>
	void ConstructLowEnvironment(int num)
	{
		List<GameObject> rowObjects=new List<GameObject>();
		if (num%3==0)
		{
			//创造围墙
			ConstructUnit(wallPrefab,new Vector3(0,num,0),rowObjects);
			ConstructUnit(wallPrefab,new Vector3(9,num,0),rowObjects);

			//辅助数组
			List<int> xCoordinate = new List<int>(){1,2,3,4,5,6,7,8};

			if (num==0)
			{
				//创造player
				ConstructUnit(player,new Vector3(4,0,0));
				xCoordinate.RemoveAt(3);
			}

			//创建道具unit
			int coinNum = Random.Range(1, 3);
			GameObject prop = null;

			for (int i = 0; i < coinNum; i++)
			{
				int x = xCoordinate[Random.Range(0, xCoordinate.Count)];
				ConstructProp(new Vector3(x, num, 0),rowObjects);
				xCoordinate.Remove(x);
			}

			//创建空白块
			for (int i = 0; i < 3-coinNum; i++)
			{
				xCoordinate.RemoveAt(Random.Range(0,xCoordinate.Count));
			}


			//创建箱子
			for (int i = 0; i < xCoordinate.Count; i++)
			{
				ConstructUnit(boxPrefabs[Random.Range(0, boxPrefabs.Length)], new Vector3(xCoordinate[i], num, 0),rowObjects);
			}
		}
		else
		{
			ConstructUnit(wallPrefab,new Vector3(0,num,0),rowObjects);
			ConstructUnit(wallPrefab,new Vector3(9,num,0),rowObjects);
		}
		
		allObjects[num] = rowObjects;
	}
	
	/// <summary>
	/// 创建行数较高的每行地图
	/// </summary>
	/// <param name="num"></param>
	void ConstructHighEnvironment(int num)
	{
		List<GameObject> rowObjects=new List<GameObject>();
		if (num%3==0)
		{
			//创造围墙
			ConstructUnit(wallPrefab,new Vector3(0,num,0),rowObjects);
			ConstructUnit(wallPrefab,new Vector3(9,num,0),rowObjects);

			//辅助数组
			List<int> xCoordinate = new List<int>(){1,2,3,4,5,6,7,8};

			//创建道具unit			
			int x = xCoordinate[Random.Range(0, xCoordinate.Count)];
			ConstructProp(new Vector3(x, num, 0),rowObjects);
			xCoordinate.Remove(x);

			//创建空白块
			for (int i = 0; i <2; i++)
			{
				xCoordinate.RemoveAt(Random.Range(0,xCoordinate.Count));
			}


			//创建箱子
			for (int i = 0; i < xCoordinate.Count; i++)
			{
				ConstructUnit(boxPrefabs[Random.Range(0, boxPrefabs.Length)], new Vector3(xCoordinate[i], num, 0),rowObjects);
			}
		}
		else
		{
			ConstructUnit(wallPrefab,new Vector3(0,num,0),rowObjects);
			ConstructUnit(wallPrefab,new Vector3(9,num,0),rowObjects);
		}
		
		allObjects[num] = rowObjects;
	}


	/// <summary>
	/// 删除某行地图
	/// </summary>
	/// <param name="num"></param>
	void DestroyEnvironment(int num)
	{
		List<GameObject> list = allObjects[num];
		
		for (int i = 0; i < list.Count; i++)
		{
			Destroy(list[i]);
		}

		allObjects.Remove(num);
	}

	/// <summary>
	/// 创建单个道具方块
	/// </summary>
	/// <param name="vector"></param>
	/// <param name="list"></param>
	void ConstructProp(Vector3 vector, List<GameObject> list)
	{
		GameObject prop=null;
		switch (Random.Range(0,5))
		{
			case 0:
				prop = redProp;
				break;
			case 1:
				prop = greenProp;
				break;
			case 2:
				prop = blueProp;
				break;
			case 3:
				prop = yellowProp;
				break;
			case 4:
				prop = coin;
				break;
			default:
				break;				
		}

		if (prop!=null)
		{
			ConstructUnit(prop,vector,list);
		}
	}

	/// <summary>
	/// 创建单个方块 并将该方块加入每行的List<GameObject>
	/// </summary>
	/// <param name="go"></param>
	/// <param name="vector"></param>
	/// <param name="list"></param>
	void ConstructUnit(GameObject go, Vector3 vector,List<GameObject> list)
	{
		GameObject instance = Instantiate(go, vector, Quaternion.identity);
		instance.transform.SetParent(map);
		list.Add(instance);
	}
	
	/// <summary>
	/// 创建单个方块
	/// </summary>
	/// <param name="go"></param>
	/// <param name="vector"></param>
	void ConstructUnit(GameObject go, Vector3 vector)
	{
		GameObject instance = Instantiate(go, vector, Quaternion.identity);
		instance.transform.SetParent(map);
	}

	/// <summary>
	/// 破坏整个地图
	/// </summary>
	public void DestoryAllMap()
	{
		if (playerGameObjecct!=null)
		{
			Destroy(playerGameObjecct.gameObject);
		}



		foreach (var key in allObjects.Keys)
		{
			List<GameObject> rowList = allObjects[key];
			foreach (GameObject obj in rowList)
			{
				Destroy(obj);
			}
		}
		
		allObjects.Clear();
		

	}


	/// <summary>
	/// 初始化Prefab列表
	/// </summary>
	public void InitializePrefabs()
	{
		boxPrefabs[0] = LoadPrefab("Prefabs/Box/BlueBox");
		boxPrefabs[1] = LoadPrefab("Prefabs/Box/GreenBox");
		boxPrefabs[2] = LoadPrefab("Prefabs/Box/RedBox");
		boxPrefabs[3] = LoadPrefab("Prefabs/Box/YellowBox");
		
        wallPrefab=LoadPrefab("Prefabs/Wall/GrayWall");
		player=LoadPrefab("Prefabs/Player/Player");
		coin=LoadPrefab("Prefabs/Coin/Coin");
	
		redProp=LoadPrefab("Prefabs/Coin/redProp");
		greenProp=LoadPrefab("Prefabs/Coin/greenProp");
		blueProp=LoadPrefab("Prefabs/Coin/blueProp");
		yellowProp=LoadPrefab("Prefabs/Coin/yellowProp");

	}

	/// <summary>
	/// 导入prefab预设体
	/// </summary>
	/// <param name="path"></param>
	/// <returns></returns>
	/// <exception cref="InvalidOperationException"></exception>
	public GameObject LoadPrefab(string path)
	{
		GameObject prefab = Resources.Load<GameObject>(path);
		if (prefab!=null)
		{
			return prefab;
		}
		else
		{
			throw  new System.InvalidOperationException("没有该prefab");
		}
	}
	
}
