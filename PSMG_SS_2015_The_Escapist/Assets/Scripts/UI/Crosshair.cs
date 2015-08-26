using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

    //
    // Script for displaying crosshair
    //

    public Texture2D crosshairTexture;
    public float crosshairScale = 0.1f;
	
    void OnGUI()
    {

        if (Time.timeScale != 0)
        {
           
            if (crosshairTexture != null)
                GUI.DrawTexture(new Rect((Screen.width - crosshairTexture.width * crosshairScale) / 2, (Screen.height - crosshairTexture.height * crosshairScale) / 2, crosshairTexture.width * crosshairScale, crosshairTexture.height * crosshairScale), crosshairTexture);
        }
    }
}
