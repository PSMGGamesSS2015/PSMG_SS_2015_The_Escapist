using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public Font myFont;

    public bool optionmenu = false;
    public bool menu = true;
    bool sound = false;
    bool video = false;

    int overallVol = 6;
    int musicVol = 6;

    int fieldOfView = 80;

    public int ResX;
    public int ResY;

    private GUIStyle buttonStyle;
    private GUIStyle titleStyle;
    private GUIStyle optionTextStyle;
    private GUIStyle smalloptionTextStyle;

    public Texture on, off;

    public bool Fullscreen; //default...

    public bool vsync = true;

    public void Start()
    {
        buttonStyle = new GUIStyle();
        buttonStyle.fontSize = 64;
        buttonStyle.normal.textColor = Color.white;
        buttonStyle.font = myFont;
        buttonStyle.alignment = TextAnchor.MiddleCenter;


        titleStyle = new GUIStyle();
        titleStyle.normal.textColor = Color.white;
        titleStyle.fontSize = 130;
        titleStyle.font = myFont;
        titleStyle.alignment = TextAnchor.MiddleCenter;

        optionTextStyle = new GUIStyle();
        optionTextStyle.normal.textColor = Color.white;
        optionTextStyle.fontSize = 48;
        optionTextStyle.font = myFont;

        smalloptionTextStyle = new GUIStyle();
        smalloptionTextStyle.normal.textColor = Color.white;
        smalloptionTextStyle.fontSize = 32;
        smalloptionTextStyle.font = myFont;
    }

    // Use this for initialization
    public void ChangeScene(int sceneNum)
    {
        Application.LoadLevel(sceneNum);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void playSound(AudioSource audiosrc)
    {
        audiosrc.Play();
    }

    public void option()
    {
        optionmenu = true;
    }


    public void OnGUI()
    {
        GUI.Label(new Rect(150, Screen.height / 2 - 300, 150, 30), "The" + "\n" + "Escapist", titleStyle);
        showMenu();
        showOptionMenu();
        showSoundOptions();
        showVideoOptions();
    }

private void showMenu()
{
 if (menu)
        {

           if (GUI.Button(new Rect(Screen.width / 2 + 500, Screen.height / 2 - 320, 200, 80), "Start Game", buttonStyle))
            {

                ChangeScene(1);

            }


            if (GUI.Button(new Rect(Screen.width / 2 + 500, Screen.height / 2 - 230, 200, 80), "Options", buttonStyle))
            {


                menu = false;
                optionmenu = true;
            }


            if (GUI.Button(new Rect(Screen.width / 2 + 500, Screen.height / 2 - 140, 200, 80), "Quit", buttonStyle))
            {


                CloseGame();
            }
        }
}

private void showOptionMenu()
{
 	if (optionmenu)
        {

            if (GUI.Button(new Rect(Screen.width / 2 + 500, Screen.height / 2 - 300, 200, 80), "Audio Settings", buttonStyle ))
            {


                optionmenu = false;
                sound = true;
            }


            if (GUI.Button(new Rect(Screen.width / 2 + 500, Screen.height / 2 - 210, 200, 80), "Video Settings", buttonStyle))
            {


                optionmenu = false;
                video = true;
            }


            if (GUI.Button(new Rect(Screen.width / 2 + 500, Screen.height / 2 - 120, 200, 80), "Back", buttonStyle))
            {


                optionmenu = false;
                menu = true;
            }
        }
}

private void showVideoOptions()
{
    if (video)
    {

    GUI.Box(new Rect(0,0,Screen.width, Screen.height),"");
        
        string[] qualities = QualitySettings.names;


        GUI.Label(new Rect(Screen.width / 2 - 275, Screen.height / 2 - 120, 100, 30), "Video Quality", optionTextStyle);

        for (int i = 0; i < qualities.Length; i++)
        {


            if (GUI.Button(new Rect(Screen.width / 2 - 270 + i * 100, Screen.height / 2 - 70, 80, 40), qualities[i], smalloptionTextStyle))
            {
               QualitySettings.SetQualityLevel(i, true);
            }
        }
        


        

        GUI.Label(new Rect(Screen.width / 2 - 275, Screen.height / 2 - 20, 120, 30), "Antialiasing", optionTextStyle);

        if (GUI.Button(new Rect(Screen.width / 2 - 280 + 100, Screen.height / 2 + 30, 120, 40), "No AA", smalloptionTextStyle))
        {
            QualitySettings.antiAliasing = 0;
        }
        //2 X AA SETTINGS
        if (GUI.Button(new Rect(Screen.width / 2 - 280 + 200, Screen.height / 2 + 30, 120, 40), "2x AA", smalloptionTextStyle))
        {
            QualitySettings.antiAliasing = 2;
        }
        //4 X AA SETTINGS
        if (GUI.Button(new Rect(Screen.width / 2 - 280 + 300, Screen.height / 2 + 30, 120, 40), "4x AA", smalloptionTextStyle))
        {
            QualitySettings.antiAliasing = 4;
        }
        //8 x AA SETTINGS
        if (GUI.Button(new Rect(Screen.width / 2 - 280 + 400, Screen.height / 2 + 30, 120, 40), "8x AA", smalloptionTextStyle))
        {
            QualitySettings.antiAliasing = 8;
        }





        GUI.Label(new Rect(Screen.width / 2 - 275, Screen.height / 2 + 80, 120, 30), "Vsync", optionTextStyle);


        if (vsync)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 230, Screen.height / 2 + 95, 120, 30), on, buttonStyle))
            {
                vsync = false;
                QualitySettings.vSyncCount = 0;
            }
        }
        else
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 230, Screen.height / 2 + 95, 120, 30), off, buttonStyle))
            {
                vsync = true;
                QualitySettings.vSyncCount = 1;

            }
        }

        

        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 80, 120, 30), "Refresh Rate", optionTextStyle);


        if (GUI.Button(new Rect(Screen.width / 2 + 120, Screen.height / 2 + 90, 120, 40), "60Hz", smalloptionTextStyle))
        {
            Screen.SetResolution(ResX, ResY, Fullscreen, 60);
        }
        //120Hz
        if (GUI.Button(new Rect(Screen.width / 2 + 200, Screen.height / 2 + 90, 120, 40), "120Hz", smalloptionTextStyle))
        {
            Screen.SetResolution(ResX, ResY, Fullscreen, 120);
        }
         
       





        
        GUI.Label(new Rect(Screen.width / 2 - 275, Screen.height / 2 + 150, 120, 30), "Resolution", optionTextStyle);


        //1080p
        if (GUI.Button(new Rect(Screen.width / 2 - 280 + 100, Screen.height / 2 + 200, 120, 40), "1080p", smalloptionTextStyle))
        {
            Screen.SetResolution(1920, 1080, Fullscreen);
            ResX = 1920;
            ResY = 1080;

        }
        //720p
        if (GUI.Button(new Rect(Screen.width / 2 - 280 + 200, Screen.height / 2 + 200, 120, 40), "720p", smalloptionTextStyle))
        {
            Screen.SetResolution(1280, 720, Fullscreen);
            ResX = 1280;
            ResY = 720;

        }
        //480p
        if (GUI.Button(new Rect(Screen.width / 2 - 280 + 300, Screen.height / 2 + 200, 120, 40), "480p", smalloptionTextStyle))
        {
            Screen.SetResolution(640, 480, Fullscreen);
            ResX = 640;
            ResY = 480;


        }

        //360p
        if (GUI.Button(new Rect(Screen.width / 2 - 280 + 400, Screen.height / 2 + 200, 120, 40), "360p", smalloptionTextStyle))
        {
            Screen.SetResolution(640, 360, Fullscreen);
            ResX = 640;
            ResY = 360;


        }
         



        if (GUI.Button(new Rect(Screen.width / 2 - 120, Screen.height / 2 + 260, 220, 60), "Back", buttonStyle))
        {


            video = false;
            optionmenu = true;
        }
    }
}

    private void showSoundOptions()
    {
        if (sound)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");

            GUI.Label(new Rect(Screen.width / 2 - 110, Screen.height / 2 - 100, 100, 30), "Overall Volume", optionTextStyle);
            overallVol = (int)GUI.HorizontalSlider(new Rect(Screen.width / 2 - 50, Screen.height / 2 -35, 100, 30), overallVol, 0.0f, 10.0f);
            GUI.Label(new Rect(Screen.width / 2 -5, Screen.height / 2 -20, 100, 30), "" + overallVol, smalloptionTextStyle);
            AudioListener.volume = overallVol / 10.0f;



            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 90, 100, 30), "Back", buttonStyle))
            {
                sound = false;
                optionmenu = true;
            }
        }
    }
}


            

         