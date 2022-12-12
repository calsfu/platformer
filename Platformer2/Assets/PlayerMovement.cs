using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 2.5f;
    public float jumpSpeed = 0.0f;
    public bool canJump = true;
    private float direction = 0;

    public Transform groundCheck;
    public float groundCheckRadius;
    
    public LayerMask groundLayer;
    public LayerMask iceGroundLayer;
    public bool isTouchingGround;
    public bool isTouchingIceGround;

    public PhysicsMaterial2D bounceMaterial, normalMaterial, iceMaterial;

    private Rigidbody2D player;

    public bool isMoving;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            Debug.Log("You Won!");
        }
    }


    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxis("Horizontal");
        player.velocity = new Vector2(player.velocity.x, player.velocity.y);
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isTouchingIceGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, iceGroundLayer);
        if (player.velocity.magnitude == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        //Makes the player face the correct direction and move left or right when jumping
        if (direction > 0f)
        {
            if (!isTouchingGround && !isTouchingIceGround)
            {
                if (jumpSpeed == 0.0f && (isTouchingGround || isTouchingIceGround))
                    player.velocity = new Vector2(direction * walkSpeed, player.velocity.y);

            }
            else
                transform.localScale = Vector2.one;

        }
        else if (direction < 0f)
        {
            if (!isTouchingGround && !isTouchingIceGround)
            {
                if (jumpSpeed == 0.0f && (isTouchingGround || isTouchingIceGround))
                    player.velocity = new Vector2(direction * walkSpeed, player.velocity.y);
            }
            else
                transform.localScale = new Vector2(-1, 1);

        }

        //Bounce
        if (jumpSpeed > 0  || !isTouchingGround)
        {
            player.sharedMaterial = bounceMaterial;
        }
        else
        {
            player.sharedMaterial = normalMaterial;
        }

        if (isTouchingIceGround)
        {
            player.sharedMaterial = iceMaterial;
        }

        //Jump
        if (!isMoving)
        {
            if (Input.GetKey("space") && (isTouchingGround || isTouchingIceGround) && canJump)
            {
                jumpSpeed += 0.025f;
            }
            if (Input.GetKeyDown("space") && (isTouchingGround || isTouchingIceGround) && canJump)
            {
                player.velocity = new Vector2(0.0f, player.velocity.y);
            }
            if (jumpSpeed >= 10f && (isTouchingGround || isTouchingIceGround))
            {
                float tempx = direction * walkSpeed;
                float tempy = jumpSpeed;
                player.velocity = new Vector2(tempx, tempy);
                Invoke("ResetJump", 0.2f);
            }
            if (Input.GetKeyUp("space"))
            {
                if (isTouchingGround || isTouchingIceGround)
                {
                    if (jumpSpeed < 1f)
                    {
                        jumpSpeed = 1f;
                    }
                    player.velocity = new Vector2(direction * walkSpeed, jumpSpeed);
                    jumpSpeed = 0f;
                }
                canJump = true;
            }
        }

}
    void ResetJump()
    {
        canJump = false;
        jumpSpeed = 0;
    }
}

