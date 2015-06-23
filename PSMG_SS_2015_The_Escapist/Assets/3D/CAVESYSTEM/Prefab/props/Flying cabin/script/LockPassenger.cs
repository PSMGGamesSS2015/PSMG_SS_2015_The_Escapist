using UnityEngine;
using System.Collections;

public class LockPassenger : MonoBehaviour {
	public Camera camPlayer;
	public Camera camCine;

	
	void Start () {
	
	}
	
void OnTriggerEnter(Collider c)
	{

			camPlayer.gameObject.SetActive(false);
			camCine.gameObject.SetActive(true);

	}

	void OnTriggerExit(Collider c)
	{
	
			camPlayer.gameObject.SetActive(true);
			camCine.gameObject.SetActive(false);

	}
}
