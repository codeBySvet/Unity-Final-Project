using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private float lockRotation = 0;
    private void Awake()
    {
        //Getting references for rigid body, box collider and animator
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //Stop the player from rotating(falling over)
        transform.rotation = Quaternion.Euler(lockRotation, lockRotation, lockRotation);

        //Checking if the player is moving horizontally
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        //Fliping (mirror transform) player when they move left 
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //If space is clicked and the player is on the ground, only then they will jump
        if (Input.GetKey(KeyCode.Space))
            //Calling jump method
            Jump();

        //Seting animator parameters -
        //Sets "run" parameter in the Animator to true when a horizontal key in pressed which will mean the run animation will be played
        anim.SetBool("run", horizontalInput != 0);
        //Sets grunded parameter to true when the player is on the ground
        anim.SetBool("grounded", isGrounded());
    }

    private void Jump()
    {
        //Jump movement
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, 5);
            anim.SetTrigger("jump");
        }
        //Setting trigger and "grounded" value for the animator so that the jump animation is only played
        //when the player actually jumps
        anim.SetTrigger("jump");
        grounded = false;
    }

    private bool isGrounded()
    {
        //Uses raycasting to fire a virtual laser into an object to send back info about that object the ray hits
        //We need this for wall jump functionality and identifying when the player is on the ground
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,
        Vector2.down, 0.1f, groundLayer);


        // //If the player is in the air and the boxcast(from the above code) is trying to check what is under the player, 
        // //there is nothing under the player. It will return false, meaning the method will return false and
        // //hence the player is not grounded. And vice versa
        return raycastHit.collider != null;

    }

}