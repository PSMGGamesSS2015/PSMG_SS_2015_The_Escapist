using UnityEngine;
using System.Collections;

public class DownwardForce : MonoBehaviour {


    Rigidbody rigidbody;

	// Use this for initialization
	void Start () {

        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        rigidbody.AddForce(Vector3.up * -10);
	}
}
