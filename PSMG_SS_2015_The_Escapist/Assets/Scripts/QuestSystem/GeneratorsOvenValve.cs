using UnityEngine;
using System.Collections;

public class GeneratorsOvenValve : InteractiveObject {

    public float maxRotation = 180f;
    public float duration = 2f;
    public bool open = false;

    private bool rotationStarted = false;
    private int revereseFactor = 1;

    private enum States { closed = 0, open = 1 }
    private States state = States.closed;

    void Start()
    {
        if (open) 
        { 
            state = States.open;
            revereseFactor = -1;
        }
    }

    void Update()
    {
        if (rotationStarted)
        {
            transform.Rotate(Vector3.up * (maxRotation / duration) * revereseFactor * Time.time);
        }
    }

    public override void trigger()
    {
        StartCoroutine("startRotation");
        revereseFactor *= -1;
        switchState();
    }

    IEnumerator startRotation()
    {
        rotationStarted = true;
        yield return new WaitForSeconds(duration);
        rotationStarted = false;
    }

    private void switchState()
    {
        state = (state == States.closed) ? States.open : States.closed;
    }

    public override int getState()
    {
        return (int)(state);
    }
}
