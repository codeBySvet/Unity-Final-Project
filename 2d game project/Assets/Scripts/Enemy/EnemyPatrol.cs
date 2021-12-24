using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;

    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {   //Obtaining the way the enemy is facing initally
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {   //Once the enemy dies, stop them from moving
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        if (movingLeft)
        {   //If the edge has not been reached, keep moving 
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                //If the edge is reached, change directions
                DirectionChange();
        }
        else
        {   //If the edge has not been reached, keep moving 
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                //If the edge is reached, change directions
                DirectionChange();
        }
    }

    private void DirectionChange()
    {   //Play the idle animation shortly once at the edge, about to turn
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
            //Reversing the value of movingLeft via negation operator. This changes direction.
            movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);

        //Make the enemy is face the right diretion
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y, initScale.z);

        //Make the enemy move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }
}
