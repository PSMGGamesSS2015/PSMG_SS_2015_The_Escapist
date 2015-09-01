using UnityEngine;
using System.Collections;

public class HighlightObject : MonoBehaviour
{

    private GameObject player;
    private PlayerInventory playerInventory;
    private InteractiveObject interactiveObj;

    private Material originalMaterial;
    private bool focused = false;
    private bool active = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<PlayerInventory>();

        interactiveObj = GetComponent<InteractiveObject>();
        if (!interactiveObj) { interactiveObj = GetComponentInParent<InteractiveObject>(); }

        Material actualMaterial = GetComponent<Renderer>().material;
        originalMaterial = UnityEngine.Object.Instantiate(actualMaterial);
    }

    void Update()
    {
        if (!active) { return; }

        if (focused && !(interactiveObj && !interactiveObj.hasInteractions()))
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

    public void setActive(bool p)
    {
        active = p;
        if (!active) { GetComponent<Renderer>().material = originalMaterial; }
    }
}
