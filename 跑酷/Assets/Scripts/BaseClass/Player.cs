using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEditor;
using UnityEngine;


public class Player : MonoBehaviour
{
    //x轴运动速度
    public float xSpeed;
    //y轴运动速度
    public float ySpeed;
    //动画速度
    public const float speed=17.5f;
    //y轴运动最大速度
    public float maxYSpeed=2f;
    

    public float y;
    public float x;


    private Animator ani;
    private Rigidbody2D rigidbody;

    private Vector3 targetPos;
    
    

    
    void Awake()
    {
        xSpeed = 10f;
        ySpeed = 1f;
        xSpeed = 5f;

        targetPos = transform.position;

        x = 4;
        y = 0;

        ani = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        if (!GameManager.Instance().isDead&&!GameManager.Instance().isPause)
        {
            if (ySpeed<maxYSpeed)
            {
                int level=(int)Mathf.Floor(transform.position.y);
                ySpeed = 1 + 0.03f * level;
            }
            Move();  
        }
        
    }

    private void Move()
    {
        Vector3 rigidbodyPos=new Vector3();

        if (Input.GetKeyDown(KeyCode.A))
        {
            ani.SetTrigger("leftMove");
            targetPos = transform.position + Vector3.up * ySpeed * Time.deltaTime + Vector3.left * xSpeed;
            rigidbodyPos = Vector3.MoveTowards(rigidbody.position, targetPos, Time.deltaTime * speed);

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ani.SetTrigger("rightMove");
            targetPos = transform.position + Vector3.up * ySpeed * Time.deltaTime + Vector3.right * xSpeed;
            rigidbodyPos = Vector3.MoveTowards(rigidbody.position, targetPos, Time.deltaTime * speed);
        }
        else
        {
            targetPos = transform.position + Vector3.up * ySpeed * Time.deltaTime;
            rigidbodyPos = Vector3.MoveTowards(rigidbody.position, targetPos, Time.deltaTime * speed);
        }

        rigidbody.MovePosition(rigidbodyPos);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Prop")
        {
            Prop prop = other.gameObject.GetComponent<Prop>();
            if (prop!=null)
            {
                switch (prop.type)
                {
                    case PropType.Blue:
                        GameManager.Instance().bluePropNum++;
                        GameManager.Instance().score += 40;     
                        break;
                    case PropType.Green:
                        GameManager.Instance().greenPropNum++;
                        GameManager.Instance().score += 40;  
                        break;
                    case PropType.Red:
                        GameManager.Instance().redPropNum++;
                        GameManager.Instance().score += 40;  
                        break;
                    case PropType.Yellow:
                        GameManager.Instance().yellowPropNum++;
                        GameManager.Instance().score += 40;  
                        break;
                    case PropType.Coin:
                        GameManager.Instance().score += 100;                   
                        break;
                    default:
                        break;      
                }
                
                AudioController.Instance().SoundPlay("award");
                Destroy(other.gameObject);
            }
        }

    }
}