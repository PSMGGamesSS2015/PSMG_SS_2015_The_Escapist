using UnityEngine;

public abstract class FocusTrigger : MonoBehaviour {

    protected bool focused = false;

    public void setFocused(bool b)
    {
        focused = b;
    }
}
