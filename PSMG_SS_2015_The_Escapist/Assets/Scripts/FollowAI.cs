using UnityEngine;
using System.Collections;

public class FollowAI : MonoBehaviour
{
    public GameObject testLightOne;
    private GameObject player;

    private Transform enemy;
    
    private NavMeshAgent agent;

    private Vector3 startPosition;

    private PlayerMovement playerMovement;

    private Shadow shadowLightOne;

    public Transform[] waypoint;
    public bool loop = true;
    public float dampingLook= 6.0f;          
    public float pauseDuration = 0;     
    private float curTime;
    private int currentWaypoint = 0;
    private CharacterController character;
    

    public bool playerInSight = false;


    // A simple AI that follows the player if he reaches the sight distance of the AI.


    /// <summary>
    /// Initialisation
    /// </summary>
    void Start()
    {
        character = GetComponent<CharacterController>();
        player = GameObject.FindWithTag("Player");
        testLightOne = GameObject.Find("Sun");

        agent = gameObject.GetComponent<NavMeshAgent>();

        playerMovement = player.GetComponent<PlayerMovement>();

        shadowLightOne = testLightOne.GetComponent<Shadow>();

        enemy = transform;
        agent.speed = Constants.AI_NORMAL_SPEED;
        startPosition = this.transform.position;

    }

    /// <summary>
    /// Describes the actions of the enemy when player is in sight or not in sight
    /// </summary>
    void Update()
    {

        if (playerMovement.sneaking == false && shadowLightOne.safe == false)
        {
            float distance = Vector3.Distance(enemy.position, player.transform.position);
            if (distance == Constants.AI_RANGE && playerInSight)
            {
                enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(player.transform.position - enemy.position), Constants.AI_ROTATION_SPEED * Time.deltaTime);
                enemy.position += enemy.forward * Constants.AI_CHASING_SPEED * Time.deltaTime;


            }
            else if (distance <= Constants.AI_RANGE && distance > Constants.AI_STOP && playerInSight)
            {
                enemy.rotation = Quaternion.Slerp(enemy.rotation,
                Quaternion.LookRotation(player.transform.position - enemy.position), Constants.AI_ROTATION_SPEED * Time.deltaTime);
                enemy.position += enemy.forward * Constants.AI_CHASING_SPEED * Time.deltaTime;
            }
        }
            
           if (currentWaypoint < waypoint.Length)
            {
                    patrol();
                }
                else
                {
                    if (loop)
                    {
                        currentWaypoint = 0;
                    }
                }
            
        }
    
 
        void patrol(){
 
         Vector3 target = waypoint[currentWaypoint].position;
         target.y = transform.position.y; 
         Vector3 moveDirection = target - transform.position;
  
         if(moveDirection.magnitude < 0.5f){
             
         if (curTime == 0)
             curTime = Time.time;
         
         if ((Time.time - curTime) >= pauseDuration){
             
             currentWaypoint++;
             curTime = 0;
         }
          }else{
         var rotation= Quaternion.LookRotation(target - transform.position);
         transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampingLook);
         character.Move(moveDirection.normalized * Constants.AI_NORMAL_SPEED * Time.deltaTime);
     }  
 }
 
            
        
           
        
       


    /// <summary>
    /// Check if player reaches the sight of the enemy 
    /// </summary>
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < Constants.AI_VIEW_ANGLE * 0.5f)
            {
                playerInSight = true;
            }
        }
    }

    /// <summary>
    /// Check if player leaves the sight of the enemy
    /// </summary>
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            playerInSight = false;
    }

    /// <summary>
    /// Wandering to a random destination point
    /// </summary>
   
  
    
}

