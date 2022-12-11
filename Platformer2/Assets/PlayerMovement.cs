using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 2.5f;
    public float jumpSpeed = 0.0f;
    private bool canJump;
    private float direction = 0;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
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

        //Makes the player face the correct direction
        if (direction > 0f)
        {
            transform.localScale = Vector2.one;
            if (isTouchingGround && jumpSpeed == 0.0f)
                player.velocity = new Vector2(direction * walkSpeed, player.velocity.y);
        }
        else if (direction < 0f)
        {
            transform.localScale = new Vector2(-1, 1);
            if (isTouchingGround && jumpSpeed == 0.0f)
                player.velocity = new Vector2(direction * walkSpeed, player.velocity.y);
        }

        //Jump
        if (Input.GetKey("space") && isTouchingGround)
        {
            jumpSpeed += 0.01f;
        }

        if (Input.GetKeyDown("space") && isTouchingGround && canJump)
        {
            player.velocity = new Vector2(0.0f, player.velocity.y);
        }

        if (jumpSpeed >= 8f && isTouchingGround)
        {
            float tempx = direction*walkSpeed;
            float tempy = jumpSpeed;
            player.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", 0.2f);
        }

        if (Input.GetKeyUp("space"))
        {
            if (isTouchingGround)
            {
                player.velocity = new Vector2(direction * walkSpeed, jumpSpeed);
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

