using UnityEngine;
using System.Collections;
using System;

public class FollowAI : MonoBehaviour
{
    public int startWaypoint = 1;
    public GameObject patrolRoute;
    public GameObject alternateRoute;
    public AITrigger patrolTrigger;
    public bool loopPatrol = true;
    public bool reverseRouteForLoop = false;
    public float searchActualisationTime = 4.0f;
    public float waitingTime = 3.0f;
    public float postDiscoverySearchDuration = 10.0f;
    public GameObject questAlcohol;

    private GameObject player;
    private PlayerMovement playerMovement;
    private Animator anim;
    private CharacterController character;
    private NavMeshAgent agent;

    private int currentWaypoint;
    private bool routeReversed = false;
    private Transform[] wayPoints;
    private Transform[] alternateWayPoints;

    private Vector3 playerLastSeenPos;
    private Vector3 approxTargetPos;
    private bool patrolFinished;
    private bool waiting = false;
    private float waitStartTime;
    private bool searchHasBeenUpdated = false;
    private bool playerHasBeenDiscovered = false;
    private float detectionStartTime;
    private float searchStartTime;
    private float searchUpdateStartTime;
    private float chasingTimeOut = 20f;
    private float playerLostTime;
    private bool postSearchStarted = false;

    private AIDetection aiDetection;

    private enum States { Patroling, Waiting, Searching, Chasing, Attacking };
    private States currentState = States.Patroling;
    private bool alcoholReached = false;
    

    /// <summary>
    /// Initialisation
    /// </summary>
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        character = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        aiDetection = GetComponentInChildren<AIDetection>();

        currentWaypoint = startWaypoint - 1;
        wayPoints = createWayPoints(patrolRoute);
        if (alternateRoute) { alternateWayPoints = createWayPoints(alternateRoute); }
    }


    /// <summary>
    /// State Machine for all AI States
    /// Conditions are checked here and the current AI State is updated
    /// </summary>
    void Update()
    {
        if(questAlcohol.GetComponent<ThrowItem>().wasThrown() && !alcoholReached)
        {
            agent.destination = questAlcohol.transform.position;
            
            if(targetReached(questAlcohol.transform.position, 2f))
            {
                wayPoints = alternateWayPoints;
                patrolFinished = false;
                currentWaypoint = 0;
                alcoholReached = true;
            }

            return;
        }

        //PLAYER REACHED ATTACK RANGE
        if (aiDetection.isPlayerInAttackRange())
        {
            attack(player.transform.position);
        }

        //PLAYER WITHIN DISCOVERY RANGE
        else if (aiDetection.isPlayerDiscovered())
        {
            playerHasBeenDiscovered = true;
            playerLastSeenPos = player.transform.position;

            chase(playerLastSeenPos);
        }

        //PLAYER WITHIN DETECTION RANGE
        else if (aiDetection.isPlayerDetected())
        {
            if (playerHasBeenDiscovered)
            {
                playerLastSeenPos = player.transform.position;
                playerLostTime = Time.time;
                chase(playerLastSeenPos);
            }
            else
            {
                search(player.transform.position);
            }
        }

        //PLAYER NOT IN SIGHT
        else if (!patrolTrigger || patrolTrigger.playerTriggered())
        {
            if (playerHasBeenDiscovered)
            {
                chase(playerLastSeenPos);

                if (targetReached(playerLastSeenPos, 1f) || isTimeLimitReached(playerLostTime, chasingTimeOut))
                {
                    playerHasBeenDiscovered = false;
                    searchStartTime = Time.time;
                    postSearchStarted = true;
                }
            }
            else if (postSearchStarted)
            {
                search(playerLastSeenPos);
                if (isTimeLimitReached(searchStartTime, postDiscoverySearchDuration)) { postSearchStarted = false; }
            }
            else
            {
                patrol();
            }
        }

        //WAITING FOR TRIGGER FROM PLAYER
        else
        {
            wait();
        }
    }


    // STATE METHODS
    private void patrol()
    {
        Vector3 nextWayPointPos = wayPoints[currentWaypoint].position;
        nextWayPointPos.y = transform.position.y;

        if (!targetReached(nextWayPointPos, 0.5f) && !waiting)
        {
            walkTo(nextWayPointPos, 6f);
            currentState = States.Patroling;
        }
        else
        {
            if(!waiting)
            {
                wait();
                waitStartTime = Time.time;
                waiting = true;
            }
            else
            {
                wait();

                if (patrolFinished) return;

                if (isTimeLimitReached(waitStartTime, waitingTime) )
                {
                    waiting = false;

                    checkPatrolRouteEnd();

                    if (!routeReversed) currentWaypoint++;
                    else currentWaypoint--; 
                }
            }
        }
    }

    private void wait()
    {
        setAllAnimBoolsFalseExcept("IsIdling");
        agent.Stop();
        currentState = States.Waiting;
    }

    private void search(Vector3 target)
    {
        if (searchHasBeenUpdated)
        {
            if ((Time.time - searchUpdateStartTime) > searchActualisationTime) { searchHasBeenUpdated = false; }
        }
        else
        {
            alignTo(player.transform.position, 12f);

            approxTargetPos = target;
            approxTargetPos.x += UnityEngine.Random.Range(-15f, 15f);
            approxTargetPos.z += UnityEngine.Random.Range(-15f, 15f);

            searchUpdateStartTime = Time.time;
            searchHasBeenUpdated = true;
        }
        if (currentState == States.Waiting) { agent.Resume(); }

        if (!postSearchStarted) { walkTo(approxTargetPos, 1f); }
        else { runTo(approxTargetPos, 3f); }

        currentState = States.Searching;
    }

    private void chase(Vector3 target)
    {
        if (currentState == States.Waiting) { agent.Resume(); }
        runTo(target, 6f);
        currentState = States.Chasing;
    }

    private void attack(Vector3 target)
    {
        setAllAnimBoolsFalseExcept("IsAttacking");
        if (currentState == States.Waiting) { agent.Resume(); }

        agent.destination = target;
        agent.speed = 1f;
        currentState = States.Attacking;
    }

    private void walkTo(Vector3 target, float alignSpeed)
    {
        setAllAnimBoolsFalseExcept("IsPatroling");
        anim.Play("Walking");
        if (currentState == States.Waiting) { agent.Resume(); }
        agent.speed = 0.3f;
        agent.destination = target;
        //alignTo(target, alignSpeed);
    }

    private void runTo(Vector3 target, float alignSpeed)
    {
        setAllAnimBoolsFalseExcept("IsChasing");
        agent.speed = 1f;
        agent.destination = target;

        //alignTo(target, alignSpeed);
    }

    private void checkPatrolRouteEnd()
    {
        if (!routeReversed && currentWaypoint == wayPoints.Length-1)
        {
            if (!loopPatrol) { patrolFinished = true; return; }

            if (!reverseRouteForLoop)
            {
                currentWaypoint = -1;
            }
            else
            {
                routeReversed = true;
            }
        } 
        else if (routeReversed && currentWaypoint == 0)
        {
            routeReversed = false;
        }
    }

    private void alignTo(Vector3 targetPos, float rotationSpeed)
    {
        var rotation = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPos - transform.position), rotationSpeed * Time.deltaTime);
    }

    private float getDistanceTo(Vector3 targetPos)
    {
        Vector3 direction = targetPos - transform.position;
        return direction.magnitude;
    }

    private bool targetReached(Vector3 target, float reachDistance)
    {
        if (getDistanceTo(target) < reachDistance) { return true; }

        return false;
    }

    private bool isTimeLimitReached(float startTime, float timeLimit)
    {
        return (Time.time - startTime) > timeLimit;
    }

    private void setAllAnimBoolsFalseExcept(string boolName)
    {
        anim.SetBool("IsPatroling", false);
        anim.SetBool("IsChasing", false);
        anim.SetBool("IsAttacking", false);
        anim.SetBool("IsSearching", false);
        anim.SetBool("IsIdling", false);

        anim.SetBool(boolName, true);
    }

    private Transform[] createWayPoints(GameObject route)
    {
        Transform[] points = new Transform[route.transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = route.transform.GetChild(i);
        }

        return points;
    }

    //PUBLIC METHODS
    public bool isChasing()
    {
        return (currentState == States.Chasing);
    }
}