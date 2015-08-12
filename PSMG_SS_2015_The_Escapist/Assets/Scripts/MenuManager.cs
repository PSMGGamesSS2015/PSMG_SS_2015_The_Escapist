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

    public bool Fullscreen; //default...

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
        GUI.Label(new Rect(150, 130, 150, 30), "The" +"\n" + "Escapist", titleStyle);
        if (GUI.Toggle(new Rect(400, 400, 150, 30), false, "VSync"))
        {
            
        }
        if (menu)
        {

           

            if (GUI.Button(new Rect(Screen.width / 2 + 500, Screen.height / 2 - 300, 200, 80), "Start Game", buttonStyle))
            {

                ChangeScene(1);

            }


            if (GUI.Button(new Rect(Screen.width / 2 + 500, Screen.height / 2 - 210, 200, 80), "Options", buttonStyle))
            {


                menu = false;
                optionmenu = true;
            }


            if (GUI.Button(new Rect(Screen.width / 2 + 500, Screen.height / 2 - 120, 200, 80), "Quit", buttonStyle))
            {


                CloseGame();
            }
        }
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



        if (sound)
        {



            overallVol = (int)GUI.HorizontalSlider(new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 30), overallVol, 0.0f, 10.0f);
            GUI.Label(new Rect(Screen.width / 2 - 50 + 110, Screen.height / 2 - 5, 100, 30), "Overall: " + overallVol);
            AudioListener.volume = overallVol / 10.0f;

           

            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 90, 100, 30), "Back", buttonStyle))
            {
                sound = false;
                optionmenu = true;
            }
        }



        if (video)
        {



            string[] qualities = QualitySettings.names;

            GUILayout.BeginVertical();

            GUI.Label(new Rect(Screen.width / 2 - 35, Screen.height / 2 - 95, 100, 30), "Video Quality");

            for (int i = 0; i < qualities.Length; i++)
            {


                if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 70 + i * 53, 120, 40), qualities[i]))
                {


                    QualitySettings.SetQualityLevel(i, true);
                }
            }

            GUILayout.EndVertical();

            GUI.Label(new Rect(Screen.width / 2 + 95, Screen.height / 2 - 95, 120, 30), "Antialiasing");

            if (GUI.Button(new Rect(Screen.width / 2 + 70, Screen.height / 2 - 70, 120, 40), "No AA"))
            {
                QualitySettings.antiAliasing = 0;
            }
            //2 X AA SETTINGS
            if (GUI.Button(new Rect(Screen.width / 2 + 70, Screen.height / 2 - 25, 120, 40), "2x AA"))
            {
                QualitySettings.antiAliasing = 2;
            }
            //4 X AA SETTINGS
            if (GUI.Button(new Rect(Screen.width / 2 + 70, Screen.height / 2 + 20, 120, 40), "4x AA"))
            {
                QualitySettings.antiAliasing = 4;
            }
            //8 x AA SETTINGS
            if (GUI.Button(new Rect(Screen.width / 2 + 70, Screen.height / 2 + 65, 120, 40), "8x AA"))
            {
                QualitySettings.antiAliasing = 8;
            }

            GUI.Label(new Rect(Screen.width / 2 + 110, Screen.height / 2 + 125, 120, 30), "Vsync");


            if (GUI.Button(new Rect(Screen.width / 2 + 70, Screen.height / 2 + 150, 120, 40), "Vsync On"))
            {
                QualitySettings.vSyncCount = 1;
            }
            if (GUI.Button(new Rect(Screen.width / 2 + 70, Screen.height / 2 + 195, 120, 40), "Vsync Off"))
            {
                QualitySettings.vSyncCount = 0;
            }



            GUI.Label(new Rect(Screen.width / 2 - 167.5f, Screen.height / 2 - 95, 120, 30), "Refresh Rate");


            if (GUI.Button(new Rect(Screen.width / 2 - 190, Screen.height / 2 - 70, 120, 40), "60Hz"))
            {
                Screen.SetResolution(ResX, ResY, Fullscreen, 60);
            }
            //120Hz
            if (GUI.Button(new Rect(Screen.width / 2 - 190, Screen.height / 2 - 25, 120, 40), "120Hz"))
            {
                Screen.SetResolution(ResX, ResY, Fullscreen, 120);
            }

            GUI.Label(new Rect(Screen.width / 2 - 160, Screen.height / 2 + 35, 120, 30), "Resolution");


            //1080p
            if (GUI.Button(new Rect(Screen.width / 2 - 190, Screen.height / 2 + 60, 120, 40), "1080p"))
            {
                Screen.SetResolution(1920, 1080, Fullscreen);
                ResX = 1920;
                ResY = 1080;
              
            }
            //720p
            if (GUI.Button(new Rect(Screen.width / 2 - 190, Screen.height / 2 + 105, 120, 40), "720p"))
            {
                Screen.SetResolution(1280, 720, Fullscreen);
                ResX = 1280;
                ResY = 720;
            
            }
            //480p
            if (GUI.Button(new Rect(Screen.width / 2 - 190, Screen.height / 2 + 150, 120, 40), "480p"))
            {
                Screen.SetResolution(640, 480, Fullscreen);
                ResX = 640;
                ResY = 480;
                

            }

            //360p
            if (GUI.Button(new Rect(Screen.width / 2 - 190, Screen.height / 2 + 195, 120, 40), "360p"))
            {
                Screen.SetResolution(640, 360, Fullscreen);
                ResX = 640;
                ResY = 360;
              

            }
            

            if (GUI.Button(new Rect(Screen.width / 2 - 120, Screen.height / 2 + 250, 220, 60), "Back", buttonStyle))
            {


                video = false;
                optionmenu = true;
            }


        }
    }
}


            

         