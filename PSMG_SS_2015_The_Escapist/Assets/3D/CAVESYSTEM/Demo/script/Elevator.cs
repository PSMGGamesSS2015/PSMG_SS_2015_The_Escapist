using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {
	private GameObject elevator;
	// Use this for initialization
	void Start () {
		elevator = GameObject.Find("Elevator");
	}

	
	void OnTriggerEnter(Collider other) {
		//elevator.GetComponent<Animator> ().enabled = true;
		elevator.GetComponent<Rigidbody> ().useGravity = true;


	}
}
