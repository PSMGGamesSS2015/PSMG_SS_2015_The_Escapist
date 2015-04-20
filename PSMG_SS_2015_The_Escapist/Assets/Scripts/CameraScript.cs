using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public Camera camera1;
    public Camera camera2;

	// Use this for initialization
	void Start () {
        camera1.enabled = false;
        camera2.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("2"))
        {
            camera1.enabled = true;
            camera2.enabled = false;
        }
        else if (Input.GetKeyDown("1"))
        {
            camera1.enabled = false;
            camera2.enabled = true;
        }
	}
}
