using UnityEngine;
using System.Collections;

public class GeneratorsOvenValveTrigger : ActionTrigger {

    public InteractiveObject valve;

    private bool focused;

	void Update () {
	    if(focused && Input.GetButtonDown("Use"))
        {
            valve.trigger();
        }
	}

    public override void setFocused(bool b)
    {
        focused = b;
    }
}
