using UnityEngine;
using System.Collections;

public class UIText : MonoBehaviour {

    
	// Use this for initialization

    public Font myFont;
    private GUIStyle textStyle;
    private string str;
   
   void Start() { 
       textStyle = new GUIStyle();
        textStyle.normal.textColor = Color.white;
        textStyle.fontSize = 40;
        textStyle.font = myFont;
               textStyle.alignment = TextAnchor.MiddleCenter;
   
   }

IEnumerator AnimateText(string strComplete) { 
    int i = 0; 
    str = ""; 
    while( i < strComplete.Length ) { 
        str += strComplete[i++]; 
        yield return new WaitForSeconds(0.1F);
    } 
}

    void OnGUI() {

        GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 250, 350, 50), "");
        GUI.TextArea(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 250, 50, 50), str, textStyle);
        
    }

	public void showText(string text){
		StartCoroutine (AnimateText(text));
	}
 }
