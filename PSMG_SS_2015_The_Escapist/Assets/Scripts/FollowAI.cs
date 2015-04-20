using UnityEngine;
using System.Collections;

public class FollowAI : MonoBehaviour {

 
    private Transform player;
    private Transform enemy;
    private int rotationSpeed = 3;
    public PlayerMovement test;

    private float range = 10f;
    private float range2 = 10f;
    private float stop = 0;

    private Vector3 startPosition; 
    private float patrolSpeed = 3f; 
    private float patrolRange = 40f;
    private NavMeshAgent agent;


    // A simple AI that follows the player if he reaches the sight distance of the AI.

    void Awake() {
        enemy = transform;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;
        startPosition = this.transform.position;
        }
     
    void Start() { 
        player = GameObject.FindWithTag("Player").transform;
        }


    void Update() {
        if (test.sneaking == false)
        {
            float distance = Vector3.Distance(enemy.position, player.position);
            if (distance >= range && distance <= range2)
            {
                enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(player.position - enemy.position), rotationSpeed * Time.deltaTime);
                enemy.position += enemy.forward * patrolSpeed * Time.deltaTime;
            }
            else if (distance <= range && distance > stop)
            {
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

    void Wander(){
        Vector3 destination = startPosition + new Vector3(Random.Range(-patrolRange, patrolRange), 0, Random.Range(-patrolRange, patrolRange));
             NewDestination(destination);
         }

    private void NewDestination(Vector3 targetPoint){
             agent.SetDestination (targetPoint);
         }
     }
     
