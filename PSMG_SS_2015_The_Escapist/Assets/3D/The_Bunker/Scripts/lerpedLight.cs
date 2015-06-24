using UnityEngine;
using System.Collections;

public class lerpedLight : MonoBehaviour {

	public Color distortColor = Color.white;
	private Color baseColor = Color.white;

	public float blinkFrequency = 1f;
	private float lerper = 0f;
	private Light myLight;

	private int direction = 1;

	void Start()
	{
		myLight = gameObject.GetComponent< Light >();
		baseColor = myLight.color;
	}

	void Update()
	{

		if( direction > 0f ) lerper += blinkFrequency * Time.deltaTime;
		else if( direction < 0f ) lerper -= blinkFrequency * Time.deltaTime;

		myLight.color = Color.Lerp( baseColor, distortColor, lerper );

		if( myLight.color == distortColor ) direction = -1;
		if( myLight.color == baseColor ) direction = 1;
	}
}
