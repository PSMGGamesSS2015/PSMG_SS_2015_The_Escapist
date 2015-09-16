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
             UIText uiText = GameObject.Find("HUD").GetComponent<UIText>();
             if (!uiText.showsText())
             {
                 uiText.showText(texts);
                 enabled = false;
             }
		}
	}
}
