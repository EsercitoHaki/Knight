using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveming : MonoBehaviour
{
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    private Rigidbody2D body;
    private Animator animator;
    private float horizontalInput;
    private bool grounded;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
       horizontalInput = Input.GetAxis("Horizontal");
       Run();
       Flip();
        if(Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
            grounded = false;
        }
    }
    
    void Run()
    {
        body.velocity = new Vector2(horizontalInput * runSpeed, body.velocity.y);
        animator.SetBool("isRunning", horizontalInput != 0);
    }

    void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        animator.SetBool("isJumping", true);
    }

    void Flip()
    {
        if(horizontalInput > 0.01F)
        {
            transform.localScale = Vector3.one;
        }else if(horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
            animator.SetBool("isJumping", false);
        }
    }
}
