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
       textStyle.fontSize = 28;
       textStyle.font = myFont;
       textStyle.alignment = TextAnchor.MiddleCenter;
   
   }

   IEnumerator AnimateText(string[] strComplete)
   {
       for (int j = 0; j < strComplete.Length; j++) {
       
       int i = 0;
           string curString = strComplete[j];
           str = "";
           while (i < curString.Length)
           {
               str += curString[i++];
               yield return new WaitForSeconds(0.05F);
           }
           yield return new WaitForSeconds(1F);
       }
       showStoryText = false;
       
   }

    void OnGUI() {
        if (showStoryText)
        {
            GUI.Box(new Rect(Screen.width / 2-320, Screen.height / 2 + 250, 600, 50), "");
            GUI.TextArea(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 250, 50, 50), str, textStyle);
        }
    }

    public void showText(string[] texts)
    {
        showStoryText = true;
        StartCoroutine(AnimateText(texts));
    }
        
 }
