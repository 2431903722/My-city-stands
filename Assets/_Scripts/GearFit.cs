using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearFit : MonoBehaviour
{
    public GameObject player;
    private CircleCollider2D coll;

    //��ת�ٶ�
    public float rotationSpeed = 1;
    //���ְ뾶
    private float gearRadius;
    //����Ƿ񿿽�ʱ��
    public bool playIsNear = false;
    //������ת
    private bool canRotate = false;
    //�����ٶ�
    public float adsorptionSpeed;
    //��Ҳ㼶
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

    //����
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

    //������ת
    void Rotate()
    {

    }

    //�������Ƿ񿿽�����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
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
