using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float direction;
    private bool hit;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {//Getting the reference for the animator and box collider scripts
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        //Checking if the projective hit anything. If yes - do nothing
        if (hit) return;
        //Else nothing is hit, so moving the fireball horizontally until target is hit or it depreciates
        float movementSpeed = 10 * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        //Depreciates the projectile over 3 seconds
        lifetime += Time.deltaTime;
        if (lifetime > 3) Deactivate();
    }
    // When the fireball hits another object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        //No longer need box colider
        boxCollider.enabled = false;
        //Playing explosion animation
        anim.SetTrigger("explode");

        if(collision.tag == "Enemy")
        collision.GetComponent<Health>().TakeDamage(1);
    }
    //This method tells the fireball to move either correctly (left or right) once fired from the player
    public void SetDirection(float _direction)
    {
        //Lifetime of projective (used for calculating when to deactiveate the object)
        lifetime = 0;
        //Direction of projectile
        direction = _direction;
        //Resetting hit state (needed for projectiles being fired after a different projectile  hits)
        hit = false;

        //Need to activate the game object once initally fired (it gets deactivated after some time from the Update method)
        gameObject.SetActive(true);

        //Box collider of projectile to detect when it hits something
        boxCollider.enabled = true;

        //Ensuring that the fireball is facing the right way when fired:
        //Making a variable to determine the direction the character is facing when the projectile is fired
        float localScaleX = transform.localScale.x;
        //If the direction of the player does not match the direction of the projectile, flip it (by giving it the opposite math sign)
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    //Used to disable the projectile once its ended.
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}