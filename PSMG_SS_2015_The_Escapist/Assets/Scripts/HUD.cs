using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    private string bottleText, stoneText, gearwheelText, pipeText, text;
    private int test = 3;
    public Texture2D bottle, stone, gearwheel, pipe, closedlock, openlock;

    private bool showInv, showLock;

    public bool showText;
	// Use this for initialization
	void Start () {
        showText = false;
        showInv = true;
        showLock = true;
        text = "Wo bin ich?";
        bottleText = "" + test;
        stoneText = "" + test;
        gearwheelText = "" + test;
        pipeText = "" + test;
	}
	
	// Update is called once per frame
	void Update () {
       

       
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
            GUI.TextArea(new Rect(Screen.width / 2 - 130, Screen.height / 2 + 200, 280, 50), text);
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
