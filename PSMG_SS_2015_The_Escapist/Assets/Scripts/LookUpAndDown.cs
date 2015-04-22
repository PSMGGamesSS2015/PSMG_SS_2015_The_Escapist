using UnityEngine;
using System.Collections;

public class LookUpAndDown : MonoBehaviour {

    private float rotationSpeed = 50f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        GetComponent<Camera>().transform.Rotate(Vector3.left * Time.deltaTime * Input.GetAxis("Mouse Y") * rotationSpeed);
	}
}
