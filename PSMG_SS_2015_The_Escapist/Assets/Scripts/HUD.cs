using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    private string bottleText, stoneText, gearwheelText, pipeText, text;
    private int test = 3;
    public Texture2D bottle, stone, gearwheel, pipe, closedlock, openlock, whiteBorder;
    private int itemNr = 1;
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

        itemKeyCheck();

       
	}

    private void itemKeyCheck()
    {
        if (Input.GetButtonDown("Bottle"))
            itemNr = 1;
        else if (Input.GetButtonDown("Stone"))
            itemNr = 2;
        else if (Input.GetButtonDown("Gearwheel"))
            itemNr = 3;
        else if (Input.GetButtonDown("Pipe"))
            itemNr = 4;



    }

 

    void OnGUI()
    {
       
        checkSelectedItem();

        inventory();
        lockpicking();
        texting();

    }

    private void checkSelectedItem()
    {
        switch (itemNr)
        {
            case 1:
                GUI.DrawTexture(new Rect(5, 595, 45, 60), whiteBorder);
                break;
            case 2:
                GUI.DrawTexture(new Rect(50, 595, 45, 60), whiteBorder);
                break;
            case 3:
                GUI.DrawTexture(new Rect(95, 595, 45, 60), whiteBorder);
                break;
            case 4:
                GUI.DrawTexture(new Rect(140, 595, 45, 60), whiteBorder);
                break;
            default:
                break;
        }
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
            GUI.Label(new Rect(10, 603, 50, 40), bottle);
            GUI.Label(new Rect(32, 620, 50, 30), bottleText);

            GUI.Label(new Rect(50, 610, 30, 50), stone);
            GUI.Label(new Rect(82, 620, 50, 30), stoneText);

            GUI.Label(new Rect(100, 615, 22, 50), gearwheel);
            GUI.Label(new Rect(127, 620, 50, 30), gearwheelText);

            GUI.Label(new Rect(145, 613, 25, 30), pipe);
            GUI.Label(new Rect(172, 620, 50, 30), pipeText);
        }
    }
}
