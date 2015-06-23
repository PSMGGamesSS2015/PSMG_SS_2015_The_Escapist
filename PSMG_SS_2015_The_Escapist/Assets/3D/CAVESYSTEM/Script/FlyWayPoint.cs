using UnityEngine;
using System.Collections;

public class FlyWayPoint : MonoBehaviour {
	public float speed;
	public float speedRotation;
	public int distanceFromWaypoint;
	private Transform Goal;
	private Vector3 velocity;
	private float distanceNextWaypoint;
	private int indexWaypoint = 0;
	public Transform[] Waypoint;
	// Use this for initialization
	void Start () {
		//Goal = GameObject.Find("Waypoint1").transform;
		Goal = Waypoint [0];
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 myPosition = transform.position;
//Distance to next waypoint
		distanceNextWaypoint = Vector3.Distance(Goal.position, transform.position);
//		print (distanceNextWaypoint);
// Determine next Waypoint
		if (distanceNextWaypoint <= distanceFromWaypoint)
		{
		indexWaypoint += 1;
			if ( Waypoint.Length < indexWaypoint )
			{
				indexWaypoint = 0;
			}
			if (indexWaypoint < Waypoint.Length)
			Goal = Waypoint [indexWaypoint];
		}
//Direction
		velocity = Goal.position -  myPosition;
//Speed
		velocity = velocity * speed;
//Move to and rotation
//		transform.LookAt (Goal);
//		transform.rotation = Quaternion.LookRotation(velocity * Time.deltaTime);
		var targetRotation = Quaternion.LookRotation(Goal.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, Goal.position, step);
//		transform.Translate(velocity * Time.deltaTime, Space.World);

	}
}
