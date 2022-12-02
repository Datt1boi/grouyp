using System;
using UnityEngine;
using System.Collections;

public class WallJump : MonoBehaviour
{

    public bool onGround = false;
    public bool jump = false;
    public float maxSpeedX = 3f;
    public float maxSpeedY = 3f;
    public LayerMask whatIsGround;
    public Transform groundChecker;

    private float groundRadius = 0.1f;
    private bool facingRight = true;
    public bool touchLeft = false;
    public bool touchRight = false;
    private CircleCollider2D circleCollider;

    public float velocityX;
    public float velocityY;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float forceX = 0;
        float forceY = 0;

        checkWallPosition();

        onGround = Physics2D.OverlapCircle(groundChecker.position, groundRadius, whatIsGround);
        float moveH = Input.GetAxisRaw("Horizontal");
        jump = Input.GetButtonDown("Jump");

        if (onGround)
        {
            if (moveH != 0)
            {
                forceX = maxSpeedX * moveH;
            }
            if (jump && !touchLeft && !touchRight)
            {
                forceY = maxSpeedY;
            }
            else if (jump && (touchLeft || touchRight))
            {
                if (touchLeft)
                {
                    forceX = maxSpeedX;
                }
                else
                {
                    forceX = -maxSpeedX;
                }
                forceY = maxSpeedY;
                Flip();
            }
            else
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
            }
        }


        else
        {
            if (!jump && !touchLeft && !touchRight)
            {
                forceY = GetComponent<Rigidbody2D>().velocity.y;
                forceX = GetComponent<Rigidbody2D>().velocity.x;
            }
            else if (jump && (touchLeft || touchRight))
            {
                if (touchLeft)
                {
                    forceX = maxSpeedX;
                    if (!facingRight)
                        Flip();
                }
                else
                {
                    forceX = -maxSpeedX;
                    if (facingRight)
                        Flip();
                }
                forceY = maxSpeedY;
            }
            else if (touchLeft || touchRight)
            {
                if (moveH == 0)
                {
                    forceX = 0;
                    forceY = GetComponent<Rigidbody2D>().velocity.y;
                }
                else
                {
                    if ((touchLeft && moveH == -1) || (touchRight && moveH == 1))
                    {
                        if (moveH == -1 && facingRight)
                        {
                            Flip();
                        }
                        if (moveH == 1 && !facingRight)
                        {
                            Flip();
                        }
                        forceY = GetComponent<Rigidbody2D>().velocity.y / 3;
                    }
                }
            }
        }


        GetComponent<Rigidbody2D>().velocity = new Vector2(forceX, forceY);


        if (onGround)
        {
            if (moveH > 0 && !facingRight)
            {
                Flip();
            }
            else if (moveH < 0 && facingRight)
            {
                Flip();
            }
        }

    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void checkWallPosition()
    {
        touchLeft = touchRight = false;
        float yCircle = NewMethod();
        Vector3 circlePosition = new Vector3(transform.localPosition.x, yCircle, transform.localPosition.z);
        bool hitRight = Physics2D.Raycast(circlePosition, transform.right, circleCollider.radius * transform.localScale.y + transform.localScale.y, 1 << LayerMask.NameToLayer("Walls"));
        bool hitLeft = Physics2D.Raycast(circlePosition, -transform.right, circleCollider.radius * transform.localScale.y + transform.localScale.y, 1 << LayerMask.NameToLayer("Walls"));
        if (hitRight)
        {
            touchRight = true;
        }
        if (hitLeft)
        {
            touchLeft = true;
        }
    }

    private float NewMethod() => transform.localPosition.y + (circleCollider.offset.y * transform.localScale.y);
}