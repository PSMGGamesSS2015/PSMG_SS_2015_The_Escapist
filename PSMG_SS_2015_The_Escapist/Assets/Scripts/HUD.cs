using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    private string bottleText, stoneText, gearwheelText, pipeText, text;
    private int test = 3;
    public Texture2D bottle, stone, gearwheel, pipe, closedlock, openlock;

    private bool showInv, showLock;

    public bool showText;

    public Font myFont;
    private GUIStyle textStyle;

	// Use this for initialization
	void Start () {

        textStyle = new GUIStyle();
        textStyle.normal.textColor = Color.white;
        textStyle.fontSize = 40;
        textStyle.font = myFont;

        showText = false;
        showInv = true;
        showLock = false;
        text = "Wo bin ich?";
        bottleText = "" + test;
        stoneText = "" + test;
        gearwheelText = "" + test;
        pipeText = "" + test;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Use Hairpin"))
        {
            showLock = !showLock;
        }
       
	}

    void OnGUI()
    {
       

        inventory();
        lockpicking();
        texting();

    }

    private void texting()
    {
        if (showText)
        {
            GUI.Box(new Rect(Screen.width / 2 - 130, Screen.height / 2 + 250, 280, 50), "");
            GUI.TextArea(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 250, 50, 50), text, textStyle);
        }
    }

    private void lockpicking()
    {
        if (showLock)
        {
            GUI.Box(new Rect(Screen.width / 2 - 130, Screen.height / 2 + 50, 280, 50), "");
            GUI.Label(new Rect(Screen.width/2 - 120, Screen.height/2 + 50, 50, 50), openlock);
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 50, 50, 50), openlock);
            GUI.Label(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 50, 50, 50), openlock);
            GUI.Label(new Rect(Screen.width / 2 + 90, Screen.height / 2 + 50, 50, 50), openlock);
        }
    }

    private void inventory()
    {
        if (showInv)
        {
            GUI.Label(new Rect(10, 600, 50, 50), bottle);
            GUI.Label(new Rect(35, 620, 50, 30), bottleText);

            GUI.Label(new Rect(50, 605, 50, 50), stone);
            GUI.Label(new Rect(100, 620, 50, 30), stoneText);

            GUI.Label(new Rect(120, 608, 35, 50), gearwheel);
            GUI.Label(new Rect(165, 620, 50, 30), gearwheelText);

            GUI.Label(new Rect(180, 608, 35, 50), pipe);
            GUI.Label(new Rect(220, 620, 50, 30), pipeText);
        }
    }
}
