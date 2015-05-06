using UnityEngine;
using System.Collections;

public class FollowAI : MonoBehaviour
{


    private Transform player;
    private GameObject light;
    private GameObject pplayer;

    private Transform enemy;
    private int rotationSpeed = 3;
    public PlayerMovement pm;
    public Shadow shadow;

    private float range = 10f;
    private float range2 = 10f;
    private float stop = 0;
    public bool playerInSight = false;
    public float fieldOfViewAngle = 110f;

    private Vector3 startPosition;
    private float patrolSpeed = 2f;
    private float patrolRange = 40f;
    private NavMeshAgent agent;


    // A simple AI that follows the player if he reaches the sight distance of the AI.

    void Awake()
    {

        enemy = transform;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;
        startPosition = this.transform.position;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        light = GameObject.FindWithTag("Lights");
        pplayer = GameObject.FindWithTag("Player");
        pm = player.GetComponent<PlayerMovement>();
        shadow = light.GetComponent<Shadow>();
    }


    void Update()
    {
        if (pm.sneaking == false && shadow.safe == false)
        {
            float distance = Vector3.Distance(enemy.position, player.position);
            if (distance >= range && distance <= range2 && playerInSight)
            {
                patrolSpeed = 5f;
                enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(player.position - enemy.position), rotationSpeed * Time.deltaTime);
                enemy.position += enemy.forward * patrolSpeed * Time.deltaTime;


            }
            else if (distance <= range && distance > stop && playerInSight)
            {
                patrolSpeed = 5f;
                enemy.rotation = Quaternion.Slerp(enemy.rotation,
                Quaternion.LookRotation(player.position - enemy.position), rotationSpeed * Time.deltaTime);
                enemy.position += enemy.forward * patrolSpeed * Time.deltaTime;
            }
            else
            {
                InvokeRepeating("Wander", 1f, 5f);
            }

        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == pplayer)
        {
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle * 0.5f)
            {
                playerInSight = true;
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameObject.FindGameObjectWithTag("Player"))
            playerInSight = false;
    }

    void Wander()
    {

        patrolSpeed = 5f;

        Vector3 destination = startPosition + new Vector3(Random.Range(-patrolRange, patrolRange), 0, Random.Range(-patrolRange, patrolRange));
        NewDestination(destination);
    }

    private void NewDestination(Vector3 targetPoint)
    {
        agent.SetDestination(targetPoint);
    }
}

