using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables:
    public float moveForce = 7f;
    private float jumpForce = 19f;
    private float floatMoveForce = 4.2f;
    private float airFlipForce = 0.02f;
    private float fallingSpeed = 6.5f;
    private float ascendingSpeed = 2f;
    private float doubleJumpForce = 23f;
    private float feetOffset = 0;
    private float moveInput;
    private int jumpsLeft;
    public Vector2 direction = Vector2.zero;
    private float oldDirection = 0;
    public LayerMask groundIsHere;
    public Animator animator;

    //Components:
    private Rigidbody2D playerRb;
    private SpriteRenderer srender;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        srender = GetComponent<SpriteRenderer>();
        CapsuleCollider2D collider = GetComponent<CapsuleCollider2D>();
        feetOffset = ((collider.size.y * transform.localScale.y) / 2) + 0.01f;

    }

    private bool IsOnGround()
    {
        Vector2 playerFeet = new Vector2(transform.position.x, transform.position.y - feetOffset);
        RaycastHit2D hit = Physics2D.Raycast(playerFeet, Vector2.down, 1f, groundIsHere);
        Debug.DrawRay(playerFeet, Vector2.down, Color.green);
        if (hit.collider != null)
        {
            float distance = Mathf.Abs(hit.point.y - playerFeet.y);
            if (distance < 0.1f)
            {
                return true;
            }
        }
        return false;
    }

    void FixedUpdate()
    {
        Vector2 moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        if(IsOnGround())
        {
            playerRb.velocity = new Vector2(moveInput * moveForce, playerRb.velocity.y);

        } else if(!IsOnGround() && direction.x == oldDirection)
        {
            playerRb.velocity = new Vector2(moveInput * floatMoveForce, playerRb.velocity.y);

        }
               
        if((moveInput > 0) && (!srender.flipX) && (IsOnGround()))
        {
            srender.flipX = true;
        } else if((moveInput < 0) && (srender.flipX) && (IsOnGround()))
        {
            srender.flipX = false;
        }

        if(moveVector != Vector2.zero)
        {
            direction = moveVector;
            direction.Normalize();
        }

    }

    private void CheckJumpReset()
    {
        if (IsOnGround() && (playerRb.velocity.y == 0))
        {
            jumpsLeft = 2;
        }
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (jumpsLeft == 2))
        {
            // playerRb.velocity = Vector2.up * jumpForce;
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpsLeft = 1;

        }
        else if ((Input.GetKeyDown(KeyCode.Space)) && (jumpsLeft == 1))
        {
            // playerRb.velocity = Vector2.up * doubleJumpForce;
            playerRb.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);
            jumpsLeft = 0;

        }
    }

    private void PlayerVelocity()
    {
        if (playerRb.velocity.y < 0)
        {
            playerRb.velocity += Vector2.up * Physics2D.gravity.y * (fallingSpeed - 1) * Time.deltaTime;

        }
        else if (playerRb.velocity.y > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.velocity += Vector2.up * Physics2D.gravity.y * (ascendingSpeed - 1) * Time.deltaTime;
        }
    }

    private void MomentumControl()
    {
        if(playerRb.velocity.y != 0 && direction.x != oldDirection)
        {
            
            playerRb.velocity = new Vector2(airFlipForce, playerRb.velocity.y);

        }
        
        oldDirection = direction.x;
        
    }

    void Update()
    {
        CheckJumpReset();
        PlayerJump();
        PlayerVelocity();
        MomentumControl();
        

    }
}
