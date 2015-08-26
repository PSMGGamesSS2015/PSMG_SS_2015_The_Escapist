using UnityEngine;
using System.Collections;

public class TextTrigger : MonoBehaviour {

    private GameObject HUD;
    private HUD hudScript;
    
	// Use this for initialization
	void Start () {
        HUD = GameObject.Find("HUD");
        hudScript = HUD.GetComponent<HUD>();
	}

    void OnTriggerStay()
    {
        hudScript.showText = true;
    }

    void OnTriggerExit()
    {
        hudScript.showText = false;
    }
}
