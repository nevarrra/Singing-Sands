using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables:
    public float moveForce = 7f;
    private float jumpForce = 13f;
    private float floatMoveForce = 4f;
    private float doubleJumpForce = 17f;
    private float feetOffset = 0;
    private float moveInput;
    private int jumpsLeft;
    public LayerMask groundIsHere;

    //Components:
    private Rigidbody2D playerRb;
    private SpriteRenderer srender;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        srender = GetComponent<SpriteRenderer>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
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
        moveInput = Input.GetAxisRaw("Horizontal");
        if(IsOnGround())
        {
            playerRb.velocity = new Vector2(moveInput * moveForce, playerRb.velocity.y);
        } else if(!IsOnGround())
        {
            playerRb.velocity = new Vector2(moveInput * floatMoveForce, playerRb.velocity.y);

        }
       
        
        if((moveInput > 0) && (!srender.flipX))
        {
            srender.flipX = true;
        } else if((moveInput < 0) && (srender.flipX))
        {
            srender.flipX = false;
        }

    }

    void Update()
    {
        if(IsOnGround())
        {
            jumpsLeft = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && (jumpsLeft > 0))
        {
           // playerRb.velocity = Vector2.up * jumpForce;
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpsLeft--;
            
        } 
        
        if((Input.GetKeyDown(KeyCode.Space)) && (!IsOnGround()) && (jumpsLeft == 0))
        {
           // playerRb.velocity = Vector2.up * doubleJumpForce;
            playerRb.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);
            jumpsLeft--;
            
        }
    }
}
