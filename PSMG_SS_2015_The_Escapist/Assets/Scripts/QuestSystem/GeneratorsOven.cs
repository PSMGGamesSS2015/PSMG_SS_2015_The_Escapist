using UnityEngine;
using System.Collections;

public class GeneratorsOven : MonoBehaviour {

    public GeneratorsOvenFire fire;
    public GeneratorsOvenValve valve;

    private enum States { noSteam = 0, providesSteam = 1 }
    private States state = States.noSteam;

    void Update()
    {
        if (fire.getState() == 1 && valve.getState() == 1) { state = States.providesSteam; }
        else { state = States.noSteam; }
    }

    public int getState()
    {
        return (int)(state);
    }
}
