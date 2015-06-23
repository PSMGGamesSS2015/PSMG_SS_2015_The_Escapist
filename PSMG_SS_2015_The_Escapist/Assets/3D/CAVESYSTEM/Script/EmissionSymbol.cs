using UnityEngine;
using System.Collections;

public class EmissionSymbol : MonoBehaviour {
	public Color EmissionColor;
	public Texture emissTexture;
	private Material mt1;
	private Color32 myRed;
	private Color emissiveColor;
	private float redFloat = 0;
	private int etichetta = 0;
	private float scaleUI;
	private string nome;
	// Use this for initialization
	void Start () {

		mt1 = this.GetComponentInChildren<MeshRenderer> ().material;

	}


	void OnTriggerEnter(Collider other) 
	{
		//Debug.Log (other.gameObject.name);

			emissiveColor = new Color ();
			emissiveColor = EmissionColor;
			redFloat = 0.7f;
			emissiveColor = emissiveColor * Mathf.LinearToGammaSpace (redFloat);
			mt1.EnableKeyword ("_EMISSION");
			mt1.SetTexture ("_EmissionMap", emissTexture);	
			mt1.SetColor ("_EmissionColor", emissiveColor);	

	



	}


	void OnTriggerExit(Collider other) 
	{
					
			emissiveColor = new Color ();
			emissiveColor = EmissionColor;
			redFloat = 0;
			emissiveColor = emissiveColor * Mathf.LinearToGammaSpace (redFloat);
			mt1.EnableKeyword ("_EMISSION");
			mt1.SetColor ("_EmissionColor", emissiveColor);	
			
	}

	

}

