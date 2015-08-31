using UnityEngine;
using System.Collections;

public class SecretPassageLever : InteractiveObject {

    public GameObject moveableWall;
    public Light auxiliaryFireLight;
    public HighlightObject bookShelfHighlight;

    private enum States { closed = 0, open = 1 }
    private States state = States.closed;

    public override void trigger()
    {
        Animation anim = moveableWall.GetComponent<Animation>();

        if (state == States.closed)
        {
            anim.Play();
            auxiliaryFireLight.enabled = false;
            bookShelfHighlight.enabled = true;
            state = States.open;
        }
    }

    public override int getState()
    {
        return (int)(state);
    }

    public override bool hasInteractions()
    {
        return (state == States.closed);
    }
}
