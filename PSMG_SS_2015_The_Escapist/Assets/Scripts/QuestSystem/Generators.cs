using UnityEngine;


public class Generators : MonoBehaviour {

    public GameObject statusLights;

    private Light[] lights;
    string inactiveHexColor;

    public enum States { inactive = 0, active = 1 }
    public States state = States.inactive;

	void Start () 
    {
        lights = statusLights.GetComponentsInChildren<Light>();
        inactiveHexColor = ColorUtility.ToHtmlStringRGBA(lights[0].color);
	}

    public void trigger(int triggeredState) 
    {
        state = (States)(triggeredState);
        switch (state)
        {
            case States.inactive:
                switchLightsColorTo(inactiveHexColor);
                break;

            case States.active:
                switchLightsColorTo("#0DC80EFF");
                break;
        }
	}

    private void switchLightsColorTo(string hexColor)
    {
        foreach (Light light in lights)
        {
            Color newColor;
            if (ColorUtility.TryParseHtmlString(hexColor, out newColor)) { light.color = newColor; }
        }
    }

    public int getState()
    {
        return (int)(state);
    }

    public bool hasInteractions()
    {
        return false;
    }
}