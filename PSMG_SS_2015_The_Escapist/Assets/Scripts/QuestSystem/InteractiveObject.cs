using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour {

    public abstract void trigger();

    public abstract int getState();

    public abstract bool hasInteractions();
}