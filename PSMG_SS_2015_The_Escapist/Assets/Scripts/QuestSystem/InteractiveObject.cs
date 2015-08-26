using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour {

    private Material originalMaterial;
    private bool focused = false;

    void Awake()
    {
        Material actualMaterial = GetComponent<Renderer>().material;
        originalMaterial = UnityEngine.Object.Instantiate(actualMaterial);
    }

    void Update()
    {
        if (focused)
        {
            changeMaterialToFocused();
        }
        else
        {
            GetComponent<Renderer>().material = originalMaterial;
        }
    }

    private void changeMaterialToFocused()
    {
        Material actualMaterial = GetComponent<Renderer>().material;
        actualMaterial.SetColor("_Color", new Color(225f / 255f, 229f / 255f, 161f / 255f, 1));
        actualMaterial.shader = Shader.Find("Toon/Lit Outline");

    }

    public void setFocused(bool b)
    {
        focused = b;
    }

    public abstract void trigger();

    public abstract int getState();
}