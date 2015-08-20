using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{

    private string bottleText, stoneText, gearwheelText, pipeText, text;
    private int test = 3;
    public Texture2D bottle, stone, gearwheel, pipe, closedlock, openlock, whiteBorder;
    private int itemNr = 1;
    private bool showInv, showLock;

    public bool showText;

    public Font myFont;
    private GUIStyle textStyle;

    private float ScreenWidthDefault = 1920;
    private float ScreenHeightDefault = 1080;

    private float ratioWidth;
    private float ratioHeight;

    // Use this for initialization
    void Start()
    {

        ratioWidth = ScreenWidthDefault / Screen.width;
        ratioHeight = ScreenHeightDefault / Screen.height;


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
    void Update()
    {

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
        //texting();

    }

    private void checkSelectedItem()
    {
        switch (itemNr)
        {
            case 1:
                GUI.DrawTexture(new Rect(Screen.width * 0.009f, Screen.height * 0.90f, 53, 60), whiteBorder);
                break;
            case 2:
                GUI.DrawTexture(new Rect(Screen.width * 0.01f + (Screen.width * 0.025f * ratioWidth), Screen.height * 0.9f, 53, 60), whiteBorder);
                break;
            case 3:
                GUI.DrawTexture(new Rect(Screen.width * 0.01f + (Screen.width * 0.025f * ratioWidth * 2), Screen.height * 0.9f, 53, 60), whiteBorder);
                break;
            case 4:
                GUI.DrawTexture(new Rect(Screen.width * 0.01f + (Screen.width * 0.025f * ratioWidth * 3), Screen.height * 0.9f, 53, 60), whiteBorder);
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

           // if(GameController.getFirstLock())
            GUI.Label(new Rect(Screen.width / 2 - 120, Screen.height / 2 + 50, 50, 50), openlock);
           // else
            GUI.Label(new Rect(Screen.width / 2 - 120, Screen.height / 2 + 50, 50, 50), closedlock);

          //  if (GameController.getSecondLock())
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 50, 50, 50), openlock);
           // else
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 50, 50, 50), closedlock);

           // if (GameController.getThirdLock())
            GUI.Label(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 50, 50, 50), openlock);
            //else
            GUI.Label(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 50, 50, 50), closedlock);

           // if (GameController.getFourthLock())
            GUI.Label(new Rect(Screen.width / 2 + 90, Screen.height / 2 + 50, 50, 50), openlock);
            //else
            GUI.Label(new Rect(Screen.width / 2 + 90, Screen.height / 2 + 50, 50, 50), closedlock);

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
