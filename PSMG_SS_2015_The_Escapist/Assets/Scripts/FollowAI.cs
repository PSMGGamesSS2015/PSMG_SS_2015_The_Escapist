using UnityEngine;
using System.Collections;

public class FollowAI : MonoBehaviour {

    private Transform player;
    private Transform enemy;
    private int moveSpeed = 4; 
    private int rotationSpeed = 4;
    public PlayerMovement test;

    private float range = 10f;
    private float range2 = 10f;
    private float stop = 0;


    // A simple AI that follows the player if he reaches the sight distance of the AI.
    void Awake() { 
    enemy = transform;
    }

    void Start() { 
    player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (test.sneaking == false)
        {
            float distance = Vector3.Distance(enemy.position, player.position);
            if (distance >= range && distance <= range2)
            {
                enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(player.position - enemy.position), rotationSpeed * Time.deltaTime);
                enemy.position += enemy.forward * moveSpeed * Time.deltaTime;
            }
            else if (distance <= range && distance > stop)
            {
                enemy.rotation = Quaternion.Slerp(enemy.rotation,
                Quaternion.LookRotation(player.position - enemy.position), rotationSpeed * Time.deltaTime);
                enemy.position += enemy.forward * moveSpeed * Time.deltaTime;
            }
            else if (distance <= stop)
            {
                enemy.rotation = Quaternion.Slerp(enemy.rotation,
                Quaternion.LookRotation(player.position - enemy.position), rotationSpeed * Time.deltaTime);
            }
        }
    }
}

