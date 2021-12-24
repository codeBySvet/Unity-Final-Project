using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float fullHealth;
    //Access modifiers are used because this variable needs to be public, but we dont want to give other scripts the 
    //option to edit the values by just making it public alone. The Get means only this script can edit the values.
    public float currentHealth { get; private set; }
    //Animations for player being damaged or die
    private Animator anim;
    private bool dead;

    private void Awake()
    {
        //Setting starting health of player to max(3)
        currentHealth = fullHealth;
        //Linking to existing animator on player object
        anim = GetComponent<Animator>();

    }
    public void TakeDamage(float _damage)
    {

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, fullHealth);
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            if (!dead)
            {

                anim.SetTrigger("die");
                //If the player dies, disable the game objects
                if (GetComponent<PlayerMovement>() != null)
                    GetComponent<PlayerMovement>().enabled = false;

                //If the Enemy dies, disable the game objects
                if (GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                if (GetComponent<MelleEnemy>() != null)
                    GetComponent<MelleEnemy>().enabled = false;

                dead = true;
            }
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, fullHealth);
    }

}