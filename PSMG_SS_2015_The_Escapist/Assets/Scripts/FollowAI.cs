﻿using UnityEngine;
using System.Collections;

public class FollowAI : MonoBehaviour
{
    public GameObject testLightOne;
    public GameObject testLightTwo;
    private Transform enemy;
    private GameObject player;
    private NavMeshAgent agent;
    private Vector3 startPosition;

    private PlayerMovement playerMovement;
    private Shadow shadowLightOne;
    private Shadow shadowLightTwo;

    public bool playerInSight = false;

    // A simple AI that follows the player if he reaches the sight distance of the AI.

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        testLightOne = GameObject.Find("Sun");
        testLightTwo = GameObject.Find("Point light");
        agent = gameObject.GetComponent<NavMeshAgent>();

        playerMovement = player.GetComponent<PlayerMovement>();
        shadowLightOne = testLightOne.GetComponent<Shadow>();
        shadowLightTwo = testLightTwo.GetComponent<Shadow>();

        enemy = transform;
        agent.speed = Constants.AI_NORMAL_SPEED;
        startPosition = this.transform.position;

    }


    void Update()
    {
        if (shadowLightOne.enabled)
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
                else
                {
                    InvokeRepeating("Wander", 1f, 5f);
                }

            }
        }
        else
        {
            {
                if (playerMovement.sneaking == false && shadowLightTwo.safe == false)
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
                    else
                    {
                        InvokeRepeating("Wander", 1f, 5f);
                    }

                }
            }
        }
    }

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


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            playerInSight = false;
    }

    void Wander()
    {
        Vector3 destination = startPosition + new Vector3(Random.Range(-Constants.AI_PATROL_RANGE, Constants.AI_PATROL_RANGE), 0, Random.Range(-Constants.AI_PATROL_RANGE, Constants.AI_PATROL_RANGE));
        NewDestination(destination);
    }

    private void NewDestination(Vector3 targetPoint)
    {
        agent.SetDestination(targetPoint);
    }
}

