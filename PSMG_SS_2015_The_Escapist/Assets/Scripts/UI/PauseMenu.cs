using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{

    private bool paused = false;
    private GUIStyle textStyle;
    private GUIStyle titleStyle;

    public Font myFont;

    void Start()
    {
        textStyle = new GUIStyle();
        textStyle.normal.textColor = Color.white;
        textStyle.fontSize = 80;
        textStyle.font = myFont;
        textStyle.alignment = TextAnchor.MiddleCenter;


        titleStyle = new GUIStyle();
        titleStyle.normal.textColor = Color.white;
        titleStyle.fontSize = 200;
        titleStyle.font = myFont;
        titleStyle.alignment = TextAnchor.MiddleCenter;

    }
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }

    }

    void OnGUI()
    {



        if (paused == true)
        {

            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");

            GUI.Label(new Rect(Screen.width / 2, 100, 100, 100), "Pause", titleStyle);
            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 -100, 80, 20), "Resume", textStyle))
            {
                paused = false;

            }
            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 80, 20), "Main Menu", textStyle))
            {
                Application.LoadLevel(0);
            }
            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 100, 80, 20), "Quit", textStyle))
            {
                Application.Quit();
            }
        }
        if (paused == true)
        {
            Time.timeScale = 0;
        }
        if (paused == false)
        {
            Time.timeScale = 1;
        }
    }
}
