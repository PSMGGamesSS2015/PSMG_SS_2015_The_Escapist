using UnityEngine;
using System.Collections;

public class SecretPassageFuseBox : InteractiveObject {

    public Light electricLight;
    public Light fireLight;

    private enum States { active = 0, deactivated = 1 }
    private States state = States.active;

    public override void trigger()
    {
        if (state == States.active)
        {
            electricLight.enabled = false;
            fireLight.enabled = true;
            state = States.deactivated;
        }
        else
        {
            electricLight.enabled = true;
            fireLight.enabled = false;
            state = States.active;
        }
    }

    public override int getState()
    {
        return (int)(state);
    }

    public override bool hasInteractions()
    {
        return true;
    }
}
