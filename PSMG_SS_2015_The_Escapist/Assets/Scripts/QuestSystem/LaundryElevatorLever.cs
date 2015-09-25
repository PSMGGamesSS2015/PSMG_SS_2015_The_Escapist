using UnityEngine;
using System.Collections;

public class LaundryElevatorLever : InteractiveObject {

    public GameObject moveableWall;
    public string animationClipName;

    private enum States { closed = 0, open = 1 }
    private States state = States.closed;

    public override void trigger()
    {
        Animation anim = moveableWall.GetComponent<Animation>();
        AudioSource audioSrc = GetComponent<AudioSource>();

        if (state == States.closed)
        {
            anim[animationClipName].speed = 1;
            anim[animationClipName].time = 0;
            anim.Play();
            audioSrc.Play();

            state = States.open;
        }
        else
        {
            anim[animationClipName].speed = -1;
            anim[animationClipName].time = anim[animationClipName].length;
            anim.Play();
            audioSrc.Play();

            state = States.closed;
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
