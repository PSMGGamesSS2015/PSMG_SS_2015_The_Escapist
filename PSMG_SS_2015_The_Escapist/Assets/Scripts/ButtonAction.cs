using UnityEngine;
using System.Collections;

public class ButtonAction : MonoBehaviour {

    public bool isStart;
    public bool isOption;
    public bool isQuit;
	// Use this for initialization
    void OnMouseUp()
    {
        if (isStart)
        {
            Application.LoadLevel(1);
        }
       
    }
}
