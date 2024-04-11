using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveming : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    Rigidbody2D myRigidbody;
    Vector2 moveInput;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float horizontal;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Run();
        FlipSprite();
        Jump();
    }
    
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        //if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){ return; }
        
        if(value.isPressed)
        {
            myRigidbody.velocity += new Vector2 (0f, jumpSpeed);
        }
    }

    void Jump()
    {
        bool checkPlayerGround = myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        myAnimator.SetBool("isJumping", !checkPlayerGround);
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if(playerHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
}
