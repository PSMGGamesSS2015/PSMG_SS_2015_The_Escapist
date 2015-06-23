using UnityEngine;
using System.Collections;

public class DeactivateBat : MonoBehaviour {

	private GameObject bat1;
	private GameObject bat2;
	private GameObject bat3;
	private GameObject bat4;
	// Use this for initialization
	void Start () {

		bat1 = GameObject.Find("bat 1");
		bat2 = GameObject.Find("bat 2");
//		bat3 = GameObject.Find("bat 3");
//		bat4 = GameObject.Find("bat 4");
	}
	
	// Update is called once per frame
	void Update () {

	}


	void OnTriggerEnter(Collider other) {
		bat1.SetActive (false);
		bat2.SetActive (false);
//		bat3.SetActive (false);
//		bat4.SetActive (false);
		print ("bat");
	}


}
