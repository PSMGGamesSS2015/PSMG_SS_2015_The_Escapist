using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

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
}
