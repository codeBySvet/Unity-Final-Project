using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelleEnemy : MonoBehaviour
{

    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    //Using this variable to allow more accurance when moving the "in sight" area of the laser
    [SerializeField] private float colliderDistance;
    //This variable will hold the size of the enemy box collider
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    //Making sure the laser on reads objects on a single layer
    [SerializeField] private LayerMask playerLayer;
    //Inital value allows the enemy to attack right away
    private float cooldownTimer = Mathf.Infinity;
    //Stopping the enemy from falling over
    private float lockRotation = 0;

    //References
    private Animator anim;
    private Health playerHealth;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()

    {

        //Stop the player from rotating(falling over)
        transform.rotation = Quaternion.Euler(lockRotation, lockRotation, lockRotation);

        //Incrementing cooldown timer per frame
        cooldownTimer += Time.deltaTime;

        //If the player is within sight of the enemy
        if (PlayerInSight())
        {   //If the attack cooldown has passed
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;

                anim.SetTrigger("meleeAttack");
            }
        }

        //This means the enemy patrol will be enabled when not in sight, but disabled when in sight
        //The enemy will stop patroling when the Player is able to be attacked. Vice Versa
        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    private bool PlayerInSight()
    {   //Using raycasing(a virtual laser is fired at objects) to determine if the player is within sight of the enemy for an attack.
        RaycastHit2D hit =
            //Method arguments represet (origin of the laser, the area/box to fire the virtual lasers at, 
            //the angle of the area/box, the direction to fire the laser, the distance to fire the laser,
            // Layermast(the laser on reads from a single layer of objects))
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        //If the Player is in sight, get their health(as we will need to affect this value if they have are hit (this is done in the DamangePlayer method))
        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {   //This method is just to visualise (in Unity) the area/box where the player is within range of the enemy for easier editing
        Gizmos.color = Color.red;
        //Drawing the same box as in the PlayerInSight method
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {   //If the player is in sight, attack them
        if (PlayerInSight())
            //Reduce the Players health
            playerHealth.TakeDamage(damage);
    }
}
