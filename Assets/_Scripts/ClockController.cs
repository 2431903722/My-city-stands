using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FunkyCode.Light2D;

public class ClockController : MonoBehaviour
{
    public Transform hour;
    public Transform minute;
    public GameObject player;
    private CircleCollider2D coll;

    //��ת�ٶ�
    public float rotationSpeed = 1;
    //����Ƿ񿿽�ʱ��
    public bool playIsNear = false;
    //������ת
    private bool canRotate = false;
    //�����ٶ�
    public float adsorptionSpeed;
    //��Ҳ㼶
    public LayerMask playLayer;

    void Update()
    {
        Rotate();
        Adsorb();
    }

    //����
    void Adsorb()
    {
        if(playIsNear && Input.GetKey(KeyCode.E))
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

    //ʱ����ת
    void Rotate()
    {
        if (Input.GetKey(KeyCode.E) && canRotate)
        {
            hour.Rotate(0, 0, -rotationSpeed * 60);
            minute.Rotate(0, 0, -rotationSpeed);
        }
    }

    //�������Ƿ񿿽�ʱ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player")){
            playIsNear = true;
        }
    }
    //����Ƿ��˳�������Χ
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            playIsNear = false;
        }
    }
}
