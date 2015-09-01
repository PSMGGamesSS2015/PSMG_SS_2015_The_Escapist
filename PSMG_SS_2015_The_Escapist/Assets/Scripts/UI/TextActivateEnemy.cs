using UnityEngine;
using System.Collections;

public class TextActivateEnemy : MonoBehaviour {

    //
    // Script for displaying story text
    //

    public bool enabled = true;

	public string[] texts;

	void OnTriggerEnter(Collider col){
		if (enabled && col.gameObject == GameObject.FindGameObjectWithTag("Enemy"))
		{
			GameObject.Find ("HUD").GetComponent<UIText> ().showText (texts);
            enabled = false;
		}
	}
}
