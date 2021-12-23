using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;   //The position the projectile is fired from
    [SerializeField] private GameObject[] fireballs;    //Array of all the created projectils to be used with the pooling method below
    private Animator anim;
    private PlayerMovement playerMovement;

    //Setting cooldown timer to a high inital number so that the player can attack right away once spawned
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        //Getting the reference for the animator and player movment scripts
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {   //checking if left mouse button is pressed. If yes, and the player is not moving(checked in the PlayerMovement script)
        //then it is calling attack method
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown)
        {

            Attack();
        }
        //Adding a gap(cooldown) between attacks
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        //resetting cooldown timer to 0
        cooldownTimer = 0;
        //Setting the position of the fireball to be at the players position (As expected when being initally fired )
        fireballs[FindFireball()].transform.position = firePoint.position;
        //Setting direction of the fireball to match the players local direction
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
