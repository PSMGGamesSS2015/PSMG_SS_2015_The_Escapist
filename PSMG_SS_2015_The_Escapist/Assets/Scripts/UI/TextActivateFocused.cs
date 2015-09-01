using UnityEngine;
using System.Collections;

public class TextActivateFocused : InteractiveObject {

    //
    // Script for displaying story text
    //

    public string text;

    private enum States { active = 0, inactive = 1 }
    private States state = States.active;

    public override void trigger()
    {
        if(state == States.active)
        {
            GameObject.Find("HUD").GetComponent<UIText>().showText(text);
            state = States.inactive;
        }
    }

    public override int getState()
    {
        return (int)(state);
    }

    public override bool hasInteractions()
    {
        return (state == States.active);
    }
}
