using UnityEngine;
using System.Collections;

public class CentralPowerSupplyFuseBox : InteractiveObject {

    public GameObject fuseLight;
    public CentralPowerSupply powerSupply;

    private Light[] lights;

    private enum States { active = 0, deactivated = 1 }
    private States state = States.active;

    void Start()
    {
        lights = fuseLight.GetComponentsInChildren<Light>();
    }

    public override void trigger()
    {
        if (state == States.active)
        {
            setFuseLightEnabled(false);
            powerSupply.fuseBoxDeactivated();

            state = States.deactivated;
        }
    }

    private void setFuseLightEnabled(bool p)
    {
        foreach (Light l in lights)
        {
            l.enabled = p;
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
