using UnityEngine;
using System.Collections;

public class FlyCabway : MonoBehaviour {
	
	public float speedCruise;
	public float speedTakeoffLanding;
	//	public float speedRotation;
	public int WaitingTime;
	public Transform[] Waypoint;
	private float distanceLanding;
	private float speed;
	private Transform Goal;
	private Vector3 velocity;
	private float distanceNextWaypoint;
	private int indexWaypoint = 0;
	private Transform[] WaypointInternal;
	private int secArray;
	private Quaternion rot;
	private bool execMoveTo;
	private string phase;
	private int stopTime;
	private float distanceFromWaypoint;
	private AudioSource audioSource;
	private Animator anim;
	
	private Transform colliderWall1; 
	private Transform colliderWall2; 
	private Transform colliderWall3; 
	private Transform colliderWall4; 
	private Transform colliderWall5; 
	private int round;
	private bool  canCallFunction = true;
	// Use this for initialization
	void Start () 
	{
		audioSource = GetComponent<AudioSource> ();
		anim = transform.GetComponentInChildren<Animator> ();
		phase = "DEFAULT";
		execMoveTo = true;
		distanceFromWaypoint = 1;
		distanceLanding = 0.1f;
		int doubleWaypoint = Waypoint.Length * 2;
		int diffWaypoint = Waypoint.Length - 1;
		int internalLenght = doubleWaypoint + diffWaypoint;
		WaypointInternal = new Transform[internalLenght] ;
		Goal = Waypoint [0];
		
		//Ricalcola Array waypoint
		for (int i=0; i <= Waypoint.Length; i++)
		{
			
			
			//Pari
			if(i%2==0)
			{
				
				if (i > 0)
				{
					//secArray += 1;	
					if (secArray == WaypointInternal.Length)
					{
						break;
					}
					WaypointInternal[secArray] = Waypoint[i].transform.FindChild("TakeOff") as Transform;
					secArray += 1;
				}
				WaypointInternal[secArray] = Waypoint[i];
				secArray += 1;	
				if (secArray == WaypointInternal.Length)
				{
					break;
				}
				WaypointInternal[secArray] = Waypoint[i].transform.FindChild("TakeOff") as Transform;
			}
			//Dispari
			if(i%2==1)
			{
				
				
				secArray += 1;
				if (secArray == WaypointInternal.Length)
				{
					break;
				}
				WaypointInternal[secArray] = Waypoint[i].transform.FindChild("TakeOff") as Transform;
				secArray += 1;
				if (secArray == WaypointInternal.Length)
				{
					break;
				}
				WaypointInternal[secArray] = Waypoint[i];
				secArray += 1;
				if (secArray == WaypointInternal.Length)
				{
					break;
				}
				WaypointInternal[secArray] = Waypoint[i].transform.FindChild("TakeOff") as Transform;
				secArray += 1;
			}
			
			//Fine Pari/Dispari
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (phase);
		Vector3 myPosition = transform.position;
		//Distance to next waypoint
		distanceNextWaypoint = Vector3.Distance(Goal.position, transform.position);
		//		stopTime = WaitingTime;
		if (phase != null && phase.StartsWith("LANDING") && distanceNextWaypoint <= distanceLanding)
		{
			stopTime = WaitingTime;
			//			Debug.Log (stopTime);
			
		}
		//Pitch & rotation
		switch (phase)
		{    	
		case "LANDING": 
			audioSource.pitch -= 0.001f;
			anim.speed -= 0.001f;
			
			break;
		case "CRUISE": 
			audioSource.pitch = 1f;
			anim.speed = 1;
			
			break;	
		case "TAKEOFF": // if a is a string
			audioSource.pitch += 0.004f;
			anim.speed += 0.001f;
			
			break;	
		default:
			
			break;
		}
		// Determine next Waypoint
		
		if (distanceNextWaypoint <= distanceFromWaypoint )
		{
			
			indexWaypoint += 1;
			if ( WaypointInternal.Length < indexWaypoint )
			{
				round++;
				indexWaypoint = 1;
			}
			//////////////////////////////////////////Calcolo velocità crociera/atterraggio/decollo//////////////////////////
			phase = GetPhase(indexWaypoint);
			switch (phase)
			{    	
			case "LANDING": 
				//Debug.Log("LANDING");
				speed = speedTakeoffLanding;
				distanceFromWaypoint = 0;
				
				break;
			case "CRUISE": 
				//Debug.Log("CRUISE");
				speed = speedCruise;
				distanceFromWaypoint = 0;
				stopTime = 0;
				
				break;	
			case "TAKEOFF": 
				//Debug.Log("TAKEOFF");
				speed = speedTakeoffLanding;
				distanceFromWaypoint = 1;
				
				stopTime = 0;
				colliderWall1 = transform.FindChild ("colliderWall1");
				colliderWall2 = transform.FindChild ("colliderWall2");
				colliderWall3 = transform.FindChild ("colliderWall3");
				colliderWall4 = transform.FindChild ("colliderWall4");
				colliderWall5 = transform.FindChild ("colliderWall5");
				colliderWall1.gameObject.SetActive(true);
				colliderWall2.gameObject.SetActive(true);
				colliderWall3.gameObject.SetActive(true);
				colliderWall4.gameObject.SetActive(true);
				colliderWall5.gameObject.SetActive(true);
				
				break;	
			default:
				//				Debug.Log ("none of the above");
				break;
			}
			//////////////////////////////////////////END Calcolo velocità crociera/atterraggio/decollo//////////////////////////
			if (indexWaypoint < WaypointInternal.Length && indexWaypoint >= 0)
				Goal = WaypointInternal [indexWaypoint];
		}
		
		//Direction
		velocity = Goal.position -  myPosition;
		velocity = velocity * speed;
		
		if(canCallFunction)
			StartCoroutine(MoveTo(execMoveTo));	
		
	}
	
	
	
	
	string GetPhase(int actualIndex)
	{
		int preIndex;
		if (round == 1)
		{
			round = 0;
			return "CRUISE";
		}
		else
		{
			preIndex = actualIndex - 1;
		}
		
		if (actualIndex == WaypointInternal.Length)
		{
			return "DEFAULT";
		}
		
		if (preIndex < 0)
		{
			return "DEFAULT";
		}
		
		string actual = WaypointInternal [actualIndex].name;
		string pre = WaypointInternal [preIndex].name;
		
		if (actual.StartsWith("Sta")  && pre.StartsWith("TakeOff"))
		{
			return "LANDING";
		}
		
		if (actual.StartsWith("Take")  && pre.StartsWith("TakeOff"))
		{
			return "CRUISE";
		}
		
		if (actual.StartsWith("Take")  && pre.StartsWith("Sta"))
		{
			return "TAKEOFF";
		}
		
		
		
		return "ERROR";
	}
	
	IEnumerator MoveTo(bool exec)
	{
		
		
		canCallFunction= false;
		if (stopTime > 0)
		{
			
			colliderWall1 = transform.FindChild ("colliderWall1");
			colliderWall2 = transform.FindChild ("colliderWall2");
			colliderWall3 = transform.FindChild ("colliderWall3");
			colliderWall4 = transform.FindChild ("colliderWall4");
			colliderWall5 = transform.FindChild ("colliderWall5");
			colliderWall1.gameObject.SetActive(false);
			colliderWall2.gameObject.SetActive(false);
			colliderWall3.gameObject.SetActive(false);
			colliderWall4.gameObject.SetActive(false);
			colliderWall5.gameObject.SetActive(false);
		}
		
		
		yield return new WaitForSeconds(stopTime/3);	
		canCallFunction= true;
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, Goal.position, step);	
		stopTime = 0;
		
		
		
		
		
		
	}
	
	
	
}
