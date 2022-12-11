using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 3.5f;
    public float jumpBoost = 2f;
    public float jumpSpeed = 0.0f;
    private bool canJump = true;
    private float direction = 0;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public bool isTouchingGround;

    public PhysicsMaterial2D bounceMaterial, normalMaterial;

    private Rigidbody2D player;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxis("Horizontal");
        player.velocity = new Vector2(player.velocity.x, player.velocity.y);
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //Makes the player face the correct direction and move left or right when jumping
        if (direction > 0f)
        {
            
            if (!isTouchingGround)
            {
                if (jumpSpeed == 0.0f && isTouchingGround)
                    player.velocity = new Vector2(direction * walkSpeed, player.velocity.y);

            }
            else
                transform.localScale = Vector2.one;

        }
        else if (direction < 0f)
        {
            transform.localScale = new Vector2(-1, 1);
            if (!isTouchingGround)
            {
                if (jumpSpeed == 0.0f && isTouchingGround)
                    player.velocity = new Vector2(direction * walkSpeed, player.velocity.y);
            }
            else
                transform.localScale = new Vector2(-1, 1);

        }

        //Player bounces when in air
        if(jumpSpeed > 0)
        {
            player.sharedMaterial = bounceMaterial;
        }
        else
        {
            player.sharedMaterial = normalMaterial;
        }
        //Jump
        if (Input.GetKey("space") && isTouchingGround && canJump)
        {
            jumpSpeed += 0.01f;
        }
        if (Input.GetKeyDown("space") && isTouchingGround && canJump)
        {
            player.velocity = new Vector2(0.0f, player.velocity.y);
        }
        if (jumpSpeed >= 10f && isTouchingGround)
        {
            float tempx = direction * walkSpeed* jumpBoost;
            float tempy = jumpSpeed;
            player.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", 1f);
        }
        if (Input.GetKeyUp("space"))
        {
            if (isTouchingGround)
            {
                player.velocity = new Vector2(direction * walkSpeed, jumpSpeed * jumpBoost);
                jumpSpeed = 0f;
            }
            canJump = true;
        }
    }
    void ResetJump()
    {
        canJump = false;
        jumpSpeed = 0;
    }
}

