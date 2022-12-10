using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpSpeed = 5f;
    private float direction = 0f;
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


        //Makes the player face the correct direction
        if (direction > 0f)
        {
            transform.localScale = Vector2.one;
        }
        else if (direction < 0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        //Jump
        if (Input.GetKeyDown("space"))
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }



    }
}
