using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    private string bottleText, stoneText, gearwheelText, pipeText;
    private int test = 3;
    public Texture2D bottle, stone, gearwheel, pipe;
	// Use this for initialization
	void Start () {
        
        
	}
	
	// Update is called once per frame
	void Update () {
       
	   bottleText = "" + test;
       stoneText = "" + test;
       gearwheelText = "" + test;
       pipeText = "" + test;
       
	}

    void OnGUI()
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
