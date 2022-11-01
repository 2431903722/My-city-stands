using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //���
    private CircleCollider2D coll;
    private Rigidbody2D rb;

    //�ƶ�����
    public float speed = 8f;
    public float xVelocity;
    public float groundDistance = 0.2f;
    //��Ծ����
    public float jumpForce = 6.3f;
    public float jumpHoldForce = 1.9f;
    public float jumpHoldDuration = 0.1f;

    float jumpTime;
    //״̬
    private bool isOnGround;
    private bool isJumping;
    public bool canMove = true;
    //����
    public LayerMask groundLayer;
    public float footOffset = 0.4f;
    //����
    bool jumpPressed;
    bool jumpHeld;

    void Start()
    {
        coll = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    //�����ײ����
    void PhysicsCheck()
    {
        //�����Ƿ��ڵ�����
        RaycastHit2D groundCheck = Raycast(new Vector2(0f, -footOffset), Vector2.down, groundDistance, groundLayer);

        if (groundCheck)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }

    }

    void Update()
    {
        //if (canMove)
        //{
            if (Input.GetButtonDown("Jump"))
            {
                jumpPressed = true;
            }
            if (Input.GetButton("Jump"))
            {
                jumpHeld = true;
            }
        //}
    }

    private void FixedUpdate()
    {
        GroundMovement();
        MidAirMovement();
        PhysicsCheck();
    }

    //�ƶ�
    void GroundMovement()
    {
        //if (canMove)
        //{
            xVelocity = Input.GetAxis("Horizontal");//-1f 1f
            rb.velocity = new Vector2(xVelocity * speed, rb.velocity.y);
        //}
    }

    //��Ծ
    void MidAirMovement()
    {
        if (jumpPressed && isOnGround && !isJumping)
        {      
            isOnGround = false;
            isJumping = true;

            jumpTime = Time.time + jumpHoldDuration;

            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

            jumpPressed = false;
        }
        else if (isJumping)
        {
            if (jumpHeld)
            {
                rb.AddForce(new Vector2(0f, jumpHoldForce), ForceMode2D.Impulse);
                jumpHeld = false;
            }
            if (jumpTime < Time.time)
            {
                isJumping = false;
            }
        }
    }

    //������ײ
    RaycastHit2D Raycast(Vector2 offSet, Vector2 rayDiraction, float length, LayerMask layer)
    {
        Vector2 pos = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos + offSet, rayDiraction, length, layer);

        Color color = hit ? Color.red : Color.green;

        Debug.DrawRay(pos + offSet, rayDiraction * length, color);

        return hit;
    }

    ////���Ŀ��ƶ�״̬
    //public void ChangeMovementState()
    //{
    //    if (canMove)
    //    {
    //        canMove = false;
    //    }
    //    else
    //    {
    //        canMove = true;
    //    }
    //}
}
