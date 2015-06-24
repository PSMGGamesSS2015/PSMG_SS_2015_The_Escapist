using UnityEngine;
using System.Collections;

public class fanRotate : MonoBehaviour {

	public Vector3 rotationSpeed = new Vector3( 0f, 0f, 0f );

	void Update()
	{
		transform.Rotate( rotationSpeed * Time.deltaTime );
	}
}
