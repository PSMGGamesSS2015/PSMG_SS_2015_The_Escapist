using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public Camera firstPerson;
    public Camera thirdPerson;

	// Use this for initialization
	void Start () {
        firstPerson.enabled = false;
        thirdPerson.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("2"))
        {
            firstPerson.enabled = true;
            thirdPerson.enabled = false;
        }
        else if (Input.GetKeyDown("1"))
        {
            firstPerson.enabled = false;
            thirdPerson.enabled = true;
        }
	}
}
