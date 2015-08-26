using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{

    //
    // Script for displaying HUD
    //

    private GameObject gameControl;
    private GUIStyle textStyle;
    public Texture2D bottle, stone, gearwheel, pipe, closedlock, openlock, whiteBorder;
    public Font myFont;

    private string bottleText, stoneText, gearwheelText, pipeText, text;

    public int amount = 3;
    private int itemNr = 1;
    private int lockCount, unlockedLocks;

    private bool showInv, showLock;
    public bool showText, showInteraction;

    private float ScreenWidthDefault = 1920;
    private float ScreenHeightDefault = 1080;
    private float ratioWidth, ratioHeight;

    
    
    void Start()
    {

        gameControl = GameObject.Find("GameController");

        // For right Scaling -- doesn't work correctly
    
        ratioWidth = ScreenWidthDefault / Screen.width;
        ratioHeight = ScreenHeightDefault / Screen.height;

        textStyle = new GUIStyle();
        textStyle.normal.textColor = Color.white;
        textStyle.fontSize = 40;
        textStyle.font = myFont;
        textStyle.alignment = TextAnchor.MiddleCenter;

        showText = false;
        showInteraction = false;
        showInv = true;
        showLock = false;
        text = "Wo bin ich?";
        bottleText = "" + amount;
        stoneText = "" + amount;
        gearwheelText = "" + amount;
        pipeText = "" + amount;
    }

    // Update is called once per frame
    void Update()
    {
        unlockedLocks = gameControl.GetComponent<GamingControl>().getUnlockedLayerNumOfFocusedDoorLock();
        lockCount = gameControl.GetComponent<GamingControl>().getTotalLayerNumOfFocusedDoorLock();
        showLock = gameControl.GetComponent<GamingControl>().isLockPickingHudNeeded();
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
    

    }

    private void checkSelectedItem()
    {
        switch (itemNr)
        {
            case 1:
                GUI.DrawTexture(new Rect(Screen.width * 0.009f, Screen.height * 0.90f , 53, 60), whiteBorder);
                break;
            case 2:
                GUI.DrawTexture(new Rect(Screen.width * 0.01f + (Screen.width * 0.025f * ratioWidth), Screen.height * 0.9f, 53 , 60 ), whiteBorder);
                break;
            case 3:
                GUI.DrawTexture(new Rect(Screen.width * 0.01f + (Screen.width * 0.025f * ratioWidth * 2), Screen.height * 0.9f, 53 , 60 ), whiteBorder);
                break;
            case 4:
                GUI.DrawTexture(new Rect(Screen.width * 0.01f + (Screen.width * 0.025f * ratioWidth * 3), Screen.height * 0.9f, 53 , 60 ), whiteBorder);
                break;
            default:
                break;
        }
    }

  

    private void lockpicking()
    {
        if (showLock)
        {


            GUI.Box(new Rect(Screen.width / 2 - 160, Screen.height / 2 + 50, 60 + ((lockCount-1) * 60), 50), "");

            for (int i = 0; i < lockCount; i++)
            {
                if (unlockedLocks > i)
                GUI.Label(new Rect(Screen.width / 2 - 150 + i * 60, Screen.height / 2 + 50, 50, 50), openlock);
                else
                GUI.Label(new Rect(Screen.width / 2 - 150 + i * 60, Screen.height / 2 + 50, 50, 50), closedlock);
               
            }

            

        }
    }

    private void inventory()
    {
        if (showInv)
        {
            GUI.Label(new Rect(Screen.width * 0.015f, Screen.height * 0.91f, 50, 40), bottle);
            GUI.Label(new Rect(Screen.width * 0.03f, Screen.height * 0.92f, 50, 30), bottleText);

            GUI.Label(new Rect(Screen.width * 0.015f + (Screen.width * 0.022f * ratioWidth), Screen.height * 0.91f, 50 , 40 ), stone);
            GUI.Label(new Rect(Screen.width * 0.03f + (Screen.width * 0.025f * ratioWidth), Screen.height * 0.92f, 50, 30), stoneText);

            GUI.Label(new Rect(Screen.width * 0.015f + (Screen.width * 0.022f * ratioWidth * 2.1f), Screen.height * 0.91f, 30, 40), gearwheel);
            GUI.Label(new Rect(Screen.width * 0.03f + (Screen.width * 0.025f * ratioWidth * 2), Screen.height * 0.92f, 50, 30), gearwheelText);

            GUI.Label(new Rect(Screen.width * 0.015f + (Screen.width * 0.022f * ratioWidth * 3.3f), Screen.height * 0.91f, 40 , 40 ), pipe);
            GUI.Label(new Rect(Screen.width * 0.03f + (Screen.width * 0.025f * ratioWidth *3), Screen.height * 0.92f, 50, 30), pipeText);
        }
    }
}
