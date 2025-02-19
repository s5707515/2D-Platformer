using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private Animator anim;

    private BoxCollider2D boxCollider;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpHeight = 10.0f;
    [SerializeField] private float initialGravity = 4.0f;

    private float wallJumpCooldown;
    private float horizontalInput;


    // Start is called before the first frame update
    void Awake()
    {
        //References rigibody and animator from GO
        rb = GetComponent<Rigidbody2D>();
        initialGravity = rb.gravityScale;
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //PLAYER MOVEMENT

        horizontalInput = Input.GetAxis("Horizontal");

        //FLIP PLAYER WHEN MOVING LEFT OR RIGHT

        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //Input.GetAxis("Horizontal) gets a value between -1 and 1 (left and right respectively) 
        //velocity.y isnt changing



        //SET ANIMATOR PARAMETERS

        anim.SetBool("run", horizontalInput != 0); // When player is moving, use run animation
        anim.SetBool("grounded", isGrounded()); //When player is in air, use jump animation


        //WALL JUMP LOGIC

        if (wallJumpCooldown > 0.2f)
        {
          
            
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
            

            if(OnWall() && !isGrounded())
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
                //Makes it so player does not fall when holding wall
            }
            else
            {
                rb.gravityScale = initialGravity;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }

        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
    }


    private void Jump()
    {
        if(isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            anim.SetTrigger("jump"); //Triggers jump animation.
        }
        else if(OnWall() && !isGrounded())
        {
            if(horizontalInput == 0)
            {
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 20, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x),transform.localScale.y, transform.localScale.z);
            }
            else
            {
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 5, 6);
            }
            wallJumpCooldown = 0;
            

            //Mathf.Sign(transform.localScale.x) returns 1 when the player is facing right and -1 when left (this is reversed with the -)
        }


    }


    private bool isGrounded()
    {

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);  //Checks if item is just below player

        return raycastHit.collider != null; //If the collider is null, then the player is in the air.
    }

    private bool OnWall()
    {

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);  //Checks if item is just below player

        return raycastHit.collider != null; //If the collider is null, then the player is in the air.
    }

    public bool CanAttack()
    {
        return horizontalInput == 0 && isGrounded() && !OnWall();
    }

}

