using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {
	private GameObject door1;
	private GameObject door2;
	private GameObject bat1;
	private GameObject bat2;
	private GameObject bat3;
	private GameObject bat4;
	// Use this for initialization
	void Start () {
		door1 = GameObject.Find("grate 5");
		door2 = GameObject.Find("grate 6");
//		bat1 = GameObject.Find("bat 1");
//		bat2 = GameObject.Find("bat 2");
//		bat3 = GameObject.Find("bat 3");
//		bat4 = GameObject.Find("bat 4");	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		door1.GetComponent<Animator> ().enabled = true;
		door2.GetComponent<Animator> ().enabled = true;
//		bat1.GetComponent<FlyWayPoint> ().enabled = true;
//		bat2.GetComponent<FlyWayPoint> ().enabled = true;
//		bat3.GetComponent<FlyWayPoint> ().enabled = true;
//		bat4.GetComponent<FlyWayPoint> ().enabled = true;
		//bat1.SetActive (true);
		//bat2.SetActive (true);
		//print ("Open doors");
	}
}
