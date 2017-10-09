using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFellow : MonoBehaviour
{

	public Transform player;


	// Use this for initialization
	void Start ()
	{
		

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameManager.Instance().IsStartGame)
		{
			if (!GameManager.Instance().isDead&&!GameManager.Instance().isPause&&player!=null)
			{
				transform.position  =new Vector3(5.75f,player.position.y+4.5f,-10f);
			}
		}


	}

	public void FindPlayer()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
}
