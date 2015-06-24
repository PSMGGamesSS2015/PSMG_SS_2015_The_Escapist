using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{

    public bool optionmenu = false;
    public bool menu = true;
    bool sound = false;
    bool video = false;

    int sfxVol = 6;
    int musicVol = 6;

    int fieldOfView = 80;

    Color boxStyle;

    
    public static bool fullscreen = false; //default...

    public void Start()
    {
        
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

        if (menu)
        {


            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 30), "Start Game"))
            {

                ChangeScene(1);
                
            }


            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 30, 100, 30), "Options"))
            {


                menu = false;
                optionmenu = true;
            }


            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 90, 100, 30), "Quit"))
            {


                CloseGame();
            }
        }
        if (optionmenu)
        {

            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 30), "Audio Settings"))
            {


                optionmenu = false;
                sound = true;
            }


            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 30, 100, 30), "Video Settings"))
            {


                optionmenu = false;
                video = true;
            }


            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 90, 100, 30), "Back"))
            {


                optionmenu = false;
                menu = true;    
            }
        }

       

        if (sound)
        {
            


            sfxVol = (int) GUI.HorizontalSlider(new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 30), sfxVol, 0.0f, 10.0f);
            GUI.Label(new Rect(Screen.width / 2 - 50 + 110, Screen.height / 2 - 5, 100, 30), "SFX: " + sfxVol);

            musicVol = (int) GUI.HorizontalSlider(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 30, 100, 30), musicVol, 0.0f, 10.0f);
            GUI.Label(new Rect(Screen.width / 2 - 50 + 110, Screen.height / 2 + 25, 100, 30), "Music: " + musicVol);

            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 90, 100, 30), "Back"))
            {
                sound = false;
                optionmenu = true;
            }
        }

       

        if (video)
        {
           


            string[] qualities = QualitySettings.names;

            GUILayout.BeginVertical();

            for (int i = 0; i < qualities.Length; i++)
            {


                if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 40 + i * 30, 100, 30), qualities[i]))
                {


                    QualitySettings.SetQualityLevel(i, true);
                }
            }

            GUILayout.EndVertical();


            fieldOfView = (int) GUI.HorizontalSlider(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 75, 100, 20), fieldOfView, 30, 120);
            GUI.Label(new Rect(Screen.width / 2 - 50 + 110, Screen.height / 2 - 80, 100, 30), "FOV: " + fieldOfView);


            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 180, 100, 30), "Back"))
            {


                video = false;
                optionmenu = true;
            }

           
        }
    }
}

            

         