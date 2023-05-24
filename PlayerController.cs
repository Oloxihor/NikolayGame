using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if(facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if(facingRight == true && moveInput < 0)
        {
            Flip();
        }
        
         if(moveInput == 0)
        {
            anim.SetBool("isRunning",false);
        }
        else
        {
            anim.SetBool("isRunning",true);
        }
    }

    void Flip()
    {
        facingRight = ! facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

        if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0,180,0);
        }
        else if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position,checkRadius,whatIsGround);
        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("takeof");
        }
        if(isGrounded == true)
        {
            anim.SetBool("isJumping",false);
        }
        else
        {
            anim.SetBool("isJumping",true);
        }
    }
}
