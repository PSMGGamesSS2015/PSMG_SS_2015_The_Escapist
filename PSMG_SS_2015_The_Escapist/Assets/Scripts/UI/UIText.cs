using UnityEngine;
using System.Collections;

public class UIText : MonoBehaviour {


    //
    // Script displaying UIText
    //

    public Font myFont;
    private GUIStyle textStyle;

    private string str;

    private bool showStoryText;
   
   void Start() {
       showStoryText = false;

       //Define Style

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
    yield return new WaitForSeconds(2F);
    showStoryText = false;
    }

    void OnGUI() {
        if (showStoryText)
        {
            GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 250, 350, 50), "");
            GUI.TextArea(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 250, 50, 50), str, textStyle);
        }
    }

	public void showText(string text){
        showStoryText = true;
		StartCoroutine (AnimateText(text));
	}
 }
