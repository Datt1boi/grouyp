using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovement : MonoBehaviour
{
    public float speed;
    public float jump;
    private float Move;
    public bool isFacingRight;
    public Rigidbody2D rb;
    public Animator anim;

    void Start()
    {
        isFacingRight = true;
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(speed * Move, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }

        if (Move >= 0.1f || Move <= -0.1f)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (!isFacingRight && Move > 0f)
        {
            Flip();
        }
        else if (isFacingRight && Move < 0f)
        {
            Flip();
        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            anim.SetBool("isJumping", false);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            anim.SetBool("isJumping", true);
        }
    }


    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

 
}