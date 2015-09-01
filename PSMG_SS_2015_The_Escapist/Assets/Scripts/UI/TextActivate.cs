﻿using UnityEngine;
using System.Collections;

public class TextActivate : MonoBehaviour {

    //
    // Script for displaying story text
    //

    public bool enabled = true;

	public string text;

	void OnTriggerEnter(Collider col){
		if (enabled && col.gameObject == GameObject.FindGameObjectWithTag("Player"))
		{
			GameObject.Find ("HUD").GetComponent<UIText> ().showText (text);
            enabled = false;
		}
	}
}
