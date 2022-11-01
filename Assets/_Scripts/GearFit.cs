using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearFit : MonoBehaviour
{
    public GameObject player;
    private CircleCollider2D coll;

    //旋转速度
    public float rotationSpeed = 1;
    //齿轮半径
    private float gearRadius;
    //玩家是否靠近时钟
    public bool playIsNear = false;
    //可以旋转
    private bool canRotate = false;
    //吸附速度
    public float adsorptionSpeed;
    //玩家层级
    public LayerMask playLayer;

    private void Start()
    {
        coll = GetComponent<CircleCollider2D>();    
    }

    void Update()
    {
        Rotate();
        Adsorb();
    }

    //吸附
    void Adsorb()
    {
        if (playIsNear && Input.GetKey(KeyCode.E))
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, Time.deltaTime * adsorptionSpeed);

            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            canRotate = true;
        }
        else
        {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
    }

    //齿轮旋转
    void Rotate()
    {

    }

    //检测玩家是否靠近齿轮
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            playIsNear = true;
        }
    }
    //玩家是否退出吸附范围
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            playIsNear = false;
        }
    }
}
