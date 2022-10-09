using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CircleCollider2D coll;
    public Rigidbody2D rb;
    //移动参数
    public float speed = 8f;
    public float xVelocity;
    public float groundDistance = 0.2f;
    //跳跃参数
    public float jumpForce = 6.3f;
    public float jumpHoldForce = 1.9f;
    public float jumpHoldDuration = 0.1f;

    float jumpTime;
    //状态
    public bool isOnGround;
    public bool isJumping;
    //环境
    public LayerMask groundLayer;
    public float footOffset = 0.4f;
    //按键
    bool jumpPressed;
    bool jumpHeld;

    void Start()
    {
        coll = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    //检测碰撞环境
    void PhysicsCheck()
    {
        //人物是否在地面上
        RaycastHit2D groundCheck = Raycast(new Vector2(0f, -footOffset), Vector2.down, groundDistance, groundLayer);
        Debug.Log("!!!");
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
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }
        if (Input.GetButton("Jump"))
        {
            jumpHeld = true;
        }
    }

    private void FixedUpdate()
    {
        GroundMovement();
        MidAirMovement();
        PhysicsCheck();
    }

    void GroundMovement()
    {  
        xVelocity = Input.GetAxis("Horizontal");//-1f 1f
        rb.velocity = new Vector2(xVelocity * speed, rb.velocity.y);
    }

    //跳跃
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

    //射线碰撞
    RaycastHit2D Raycast(Vector2 offSet, Vector2 rayDiraction, float length, LayerMask layer)
    {
        Vector2 pos = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos + offSet, rayDiraction, length, layer);

        Color color = hit ? Color.red : Color.green;

        Debug.DrawRay(pos + offSet, rayDiraction * length, color);

        return hit;
    }
}
