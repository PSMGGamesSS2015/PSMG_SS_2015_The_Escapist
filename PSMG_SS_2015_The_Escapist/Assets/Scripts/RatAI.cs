using UnityEngine;
using System.Collections;
using System;

public class RatAI : MonoBehaviour
{
    public  GameObject player;

    private Transform enemy;





    public Transform[] waypoint;
    public bool loop = true;
    public float dampingLook = 6.0f;
    private float pauseDuration = 0f;
    private float attackingPauseDuration = 1f;
    private float curTime;
    public int currentWaypoint = 0;


    private Animator anim;


    //isWalking = 1;
    //isHissing = 2;
  
    private int state = 1;


    // A simple AI that follows the player if he reaches the sight distance of the AI.


    /// <summary>
    /// Initialisation
    /// </summary>
    void Start()
    {



        player = GameObject.Find("Player");
        //Added by Chris
        anim = GetComponent<Animator>();




        enemy = transform;

    }

    /// <summary>
    /// Describes the actions of the enemy when player is in sight or not in sight
    /// </summary>
    void Update()
    {
       
        
            
            switch (state)
            {

                case 1:
                    anim.SetBool("IsWalking", true);
                    anim.SetBool("IsAttacking", false);
                    walk();
                    break;

                case 2:
                    enemy.rotation = Quaternion.Slerp(enemy.rotation,
                 Quaternion.LookRotation(player.transform.position - enemy.position), Constants.AI_ROTATION_SPEED * Time.deltaTime);
                    anim.SetBool("IsWalking", false);
                    anim.SetBool("IsAttacking", true);
                    StartCoroutine(StartHissing());

                    break;

           
                default:
                    break;
            }


         
        
    }

  

      IEnumerator StartHissing()
    {
        yield return new WaitForSeconds(attackingPauseDuration);
        anim.SetBool("IsAttacking", false);

        anim.SetBool("IsWalking", true);
        anim.StopPlayback();
        state = 1;
        
    }
    

    

    private void walk()
    {

        if (currentWaypoint < waypoint.Length)
        {
            patrolWay();

        }
        else
        {
            if (loop)
            {
                currentWaypoint = 0;
            }
        }

    }
    void patrolWay()
    {

        
        Vector3 target = waypoint[currentWaypoint].position;
        target.y = transform.position.y;
        Vector3 moveDirection = target - transform.position;

        if (moveDirection.magnitude < 0.5)
        {
            if (curTime == 0)
                curTime = Time.time;
            if ((Time.time - curTime) >= pauseDuration)
            {
              
                currentWaypoint++;
                curTime = 0;
                
            }
        }
        else
        {

            var rotation = Quaternion.LookRotation(target - transform.position);
            transform.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(target - enemy.position), dampingLook * Time.deltaTime);
            enemy.position += enemy.forward * Constants.AI_RAT_WALKING_SPEED * Time.deltaTime;
        }
    }
        
    /// <summary>
    /// Check if player reaches the sight of the enemy 
    /// </summary>
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == player)
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsAttacking", true);
            anim.StopPlayback();
            state = 2;
        }
    }

    /// <summary>
    /// Check if player leaves the sight of the enemy
    /// </summary>
  
}





