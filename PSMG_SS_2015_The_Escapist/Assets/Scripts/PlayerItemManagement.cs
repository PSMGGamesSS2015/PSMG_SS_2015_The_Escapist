using UnityEngine;
using System.Collections;

public class PlayerItemManagement : MonoBehaviour {

    private GamingControl gamingControl;

    void Awake()
    {
        gamingControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GamingControl>();
    }

	void Update()
    {
        if (Input.GetButtonDown("Use Hairpin"))
        {
            gamingControl.hairpinActive = !gamingControl.hairpinActive;
            Debug.Log("Hairpin equipped. Now you can try to unlock Doors with your Left and Right Mouse Button for Left and Right Movement of the Hairpin!" +
            " Locked doors are furnished at least with 4 layers of security rings, which you can unlock either with Left or with Right Movement of your hairpin." +
            " If you have succes at one layer you will hear a Click Sound and return to the next Layer. If you failed you need to start at the beginning again.");
        }
    }
}