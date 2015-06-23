using UnityEngine;
using System.Collections;

public class EmissionPulse : MonoBehaviour {
	public Material[] mt1;
	//public float value;
	//public Renderer myRend;
	//private bool  canCallFunction = true;
	private Color32 myRed;
	private Color emissiveColor;
	private float redFloat = 0;
	private int etichetta = 0;
	private float scaleUI;
	// Use this for initialization
	void Start () {
		//mt1 = new Material[11];

	}
	
	// Update is called once per frame
	void FixedUpdate () {



		//scaleUI = mt1.GetFloat ( "_EmissionScaleUI");

	//	Debug.Log (redFloat);
		//etichetta  1 = RED 0 = BLACK
		if (redFloat>=3.5f)
		{
			etichetta = 1; //RED
		}

		if (redFloat<=-0.5f )
		{
			etichetta = 0; //BLACK
		}

		if(redFloat <= 50f && etichetta == 0 )
		{
			redFloat += 0.010f;
		myRed = new Color (redFloat, 0, 0);
			emissiveColor = new Color ();
			emissiveColor = Color.red;
			emissiveColor = emissiveColor * Mathf.LinearToGammaSpace (redFloat);
			for (int i=0;i <mt1.Length;i++)
			{
			//mt1.SetColor ("_EmissionColor", emissiveColor);
				mt1[i].EnableKeyword ("_EMISSION");
				mt1[i].SetColor ("_EmissionColor", emissiveColor);	
			}
		//DynamicGI.SetEmissive(myRend, myRed);
		}

		if(redFloat <= 50f && etichetta == 1 )
		{
			redFloat -= 0.025f; //0.05f
			myRed = new Color (redFloat, 255, 0);
			emissiveColor = new Color ();
			emissiveColor = Color.red;
			emissiveColor = emissiveColor * Mathf.LinearToGammaSpace (redFloat);
			for (int i=0;i <mt1.Length;i++)
			{
				//mt1.SetColor ("_EmissionColor", emissiveColor);
				mt1[i].SetColor ("_EmissionColor", emissiveColor);	
			}

			//DynamicGI.SetEmissive(myRend, myRed);
		}


	}

	
}
