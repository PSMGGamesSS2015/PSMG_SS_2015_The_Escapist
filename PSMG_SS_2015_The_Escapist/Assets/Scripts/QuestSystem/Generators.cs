using UnityEngine;
using System.Collections;

public class Generators : MonoBehaviour {

    public GameObject rightLightRows;
    public GameObject leftLightRows;

    private int state = 0;

	void Start () {
	    
	}

    public void trigger() 
    {
        switch (state)
        {
            case 0:
                rightLightRows.SetActive(true);
                leftLightRows.SetActive(true);
                state ++;
                break;

            case 1:
                Light[] lightsRight = rightLightRows.GetComponentsInChildren<Light>();
                switchLightColorToGreen(lightsRight);
                state ++;
                break;

            case 2:
                Light[] lightsLeft = leftLightRows.GetComponentsInChildren<Light>();
                switchLightColorToGreen(lightsLeft);
                state ++;
                break;
        }
	}

    private void switchLightColorToGreen(Light[] lights)
    {
        foreach (Light light in lights)
        {
            Color greenColor;
            if (Color.TryParseHexString("#0DC80EFF", out greenColor)) { light.color = greenColor; }
        }
    }
}
