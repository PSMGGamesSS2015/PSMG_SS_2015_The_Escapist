using UnityEngine;
using System.Collections;

public class TextActivate : MonoBehaviour {

	public string text;

	void OnTriggerEnter(Collider col){
		if (col.gameObject == GameObject.FindGameObjectWithTag("Player"))
		{
			GameObject.Find ("HUD").GetComponent<UIText> ().showText (text);
			col.enabled = false;
		}
	}
}
