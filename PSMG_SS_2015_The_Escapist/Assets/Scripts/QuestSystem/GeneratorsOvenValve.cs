using UnityEngine;
using System.Collections;

public class GeneratorsOvenValve : InteractiveObject {

    public float speed = 0.5f;
    public float duration = 1f;

    public enum States { closed = 0, open = 1 }
    public States state = States.closed;

    private bool rotationStarted = false;
    private int revereseFactor = 1;

    void Start()
    {
        if (state == States.open) { revereseFactor = -1; }
    }

    void FixedUpdate()
    {
        if (rotationStarted)
        {
            transform.Rotate(Vector3.up * speed * revereseFactor);
        }
    }

    public override void trigger()
    {
        if (!rotationStarted)
        {
            StartCoroutine("startRotation");
            switchState();
        }
    }

    IEnumerator startRotation()
    {
        rotationStarted = true;
        yield return new WaitForSeconds(duration);
        rotationStarted = false;
        revereseFactor *= -1;
    }

    private void switchState()
    {
        state = (state == States.closed) ? States.open : States.closed;
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
