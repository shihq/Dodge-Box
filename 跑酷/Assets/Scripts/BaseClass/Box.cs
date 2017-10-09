using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public BoxType type;
    public GameObject player;

    private void Awake()
    {
        player=GameObject.FindWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="Player")
        {
        Vector2 playerPos = player.transform.position;
        Vector2 boxPos = transform.position;

        if (Mathf.Abs(playerPos.x-boxPos.x)>0.717f)
        {
            return;
        }
        else if ((playerPos.y-boxPos.y)>0.88f)
        {
            return;
        }
        else
        {
 
            bool isContinue = false;
            switch (type)
            {
                case BoxType.Red:
                    if (GameManager.Instance().redPropNum>0)
                    {
                        GameManager.Instance().redPropNum--;
                        isContinue = true;
                    }
                    break;
                case BoxType.Blue: 
                    if (GameManager.Instance().bluePropNum>0)
                    {
                        GameManager.Instance().bluePropNum--;
                        isContinue = true;
                    }
                    break;
                case BoxType.Green:
                    if (GameManager.Instance().greenPropNum>0)
                    {
                        GameManager.Instance().greenPropNum--;
                        isContinue = true;
                    }
                    break;
                case BoxType.Yellow:
                    if (GameManager.Instance().yellowPropNum>0)
                    {
                        GameManager.Instance().yellowPropNum--;
                        isContinue = true;                        
                    }
                    break;
                default:
                    break;
            }        
        

            if (isContinue)
            {
                Destroy(this.gameObject);
                AudioController.Instance().SoundPlay("BoxExplode");
            }
            else
            {
                GameManager.Instance().isDead = true;
                GameManager.Instance().nowScore = GameManager.Instance().score;
                ContextManager.Instance().Push(new UI_GameOverMenuContext());
                
                AudioController.Instance().SoundPlay("dead");
                Destroy(player);
            }
        }
    
        }
    }
}