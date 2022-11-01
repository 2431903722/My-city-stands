using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRebirth : MonoBehaviour
{
    public Rigidbody2D rb;
    public float distance = 100f;
    public Transform RebirthPoint;
    private GameObject[] AllRebirthPoint;

    private void Start()
    {
        AllRebirthPoint = GameObject.FindGameObjectsWithTag("�����");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "������")
        {
            Invoke("Rebirth", 2f);

        }
    }
    void Rebirth()
    {
        foreach (GameObject t in AllRebirthPoint)
        {
            if (t.transform.position.x < rb.position.x && Mathf.Abs(t.transform.position.x - rb.transform.position.x) < distance)//�ҵ�����ĸ����
            {
                RebirthPoint.transform.position = t.transform.position;
                distance = Mathf.Abs(t.transform.position.x - rb.transform.position.x);
            }

        }

        rb.transform.position = RebirthPoint.transform.position;
        rb.velocity = Vector2.zero;
    }
}
