using UnityEngine;
using System.Collections;
using System;

public enum EnemyBehavior
{
    patrol, chase, search
}
public class FollowAI : MonoBehaviour
{
    public GameObject testLightOne;
    private GameObject player;

    private Transform enemy;

    private NavMeshAgent agent;

    private Vector3 startPosition;

    private PlayerMovement playerMovement;

    Shadow shadowLightOne;

    public Transform[] waypoint;
    public bool loop = true;
    public float dampingLook = 6.0f;
    private float pauseDuration = 4.0f;
    private float curTime;
    public int currentWaypoint = 0;
    private CharacterController character;

    public EnemyBehavior currentBehavior = EnemyBehavior.patrol;

    //Added by Chris
    private Animator anim;

    public bool playerInSight = false;

    public bool hasSeenPlayer = false;

    public bool test;
    public bool startGoing = false;
    // A simple AI that follows the player if he reaches the sight distance of the AI.


    /// <summary>
    /// Initialisation
    /// </summary>
    void Start()
    {
        character = GetComponent<CharacterController>();
        player = GameObject.FindWithTag("Player");
        testLightOne = GameObject.Find("Sun");

        //Added by Chris
        anim = GetComponent<Animator>();

        agent = gameObject.GetComponent<NavMeshAgent>();

        playerMovement = player.GetComponent<PlayerMovement>();

        //shadowLightOne = testLightOne.GetComponent<Shadow>();

        enemy = transform;
        //agent.speed = Constants.AI_NORMAL_SPEED;
        startPosition = character.transform.position;

    }

    /// <summary>
    /// Describes the actions of the enemy when player is in sight or not in sight
    /// </summary>
    void Update()
    {
        if (startGoing)
        {
            checkState();

            //Added by Chris
            animate();
        }
    }

    //Added by Chris
    private void animate()
    {
        bool patroling = true;
        bool chasing = false;
        bool searching = false;

        if (currentBehavior == EnemyBehavior.patrol)
        {
            patroling = true;
            anim.SetBool("IsPatroling", patroling);
        }
        else if (currentBehavior == EnemyBehavior.search)
        {

        }
        else if (currentBehavior == EnemyBehavior.chase)
        {

        }
    }

    void checkState()
    {
        if (currentBehavior == EnemyBehavior.patrol)
        {
            patrol();
        }
        else if (currentBehavior == EnemyBehavior.chase)
        {

            chase();
        }

        else if (currentBehavior == EnemyBehavior.search)
        {
            InvokeRepeating("Search", 0, 2);
            StartCoroutine("startRoutine");
        }
    }

    IEnumerator startRoutine()
    {
        yield return new WaitForSeconds(5f);
        test = true;
        nextState();
    }

    private void nextState()
    {
        if (test)
        {
            hasSeenPlayer = false;
            CancelInvoke("Search");
            StopCoroutine("startRoutine");
            test = false;
            patrol();
            currentBehavior = EnemyBehavior.patrol;

        }
    }



    private void Search()
    {
        Vector3 destination = startPosition - new Vector3(UnityEngine.Random.Range(-Constants.AI_PATROL_RANGE, Constants.AI_PATROL_RANGE), 0, UnityEngine.Random.Range(-Constants.AI_PATROL_RANGE, Constants.AI_PATROL_RANGE));
        NewDestination(destination);
    }

    private void NewDestination(Vector3 targetPoint)
    {
        agent.SetDestination(targetPoint);

    }

    private void chase()
    {
        if (playerMovement.sneaking == false)// && shadowLightOne.safe == false)
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
    }

    private void patrol()
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
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            startGoing = true;
        }
    }
}






    /*
    /// <summary>
    /// Check if player reaches the sight of the enemy 
    /// </summary>
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player && shadowLightOne.safe == false)
        {
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < Constants.AI_VIEW_ANGLE * 0.5f)
            {
                CancelInvoke("patrol");
                currentBehavior = EnemyBehavior.chase;
                playerInSight = true;
                hasSeenPlayer = true;
            }
        }
    }

    /// <summary>
    /// Check if player leaves the sight of the enemy
    /// </summary>
    void OnTriggerExit(Collider other)
    {
        
            playerInSight = false;

            if (hasSeenPlayer)
            {
                currentBehavior = EnemyBehavior.search;
            }
        
    }





    /// <summary>
    /// Wandering to a random destination point
    /// </summary>


    */


