﻿using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public string name;

    private GameObject player;
    private PlayerInventory playerInventory;
    private Material originalMaterial;
    private bool focused = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<PlayerInventory>();

        Material actualMaterial = GetComponent<Renderer>().material;
        originalMaterial = UnityEngine.Object.Instantiate(actualMaterial);
    }

    void Update()
    {
        if (focused)
        {
            changeMaterialToFocused();

            if (Input.GetButtonDown("Use"))
            {
                transform.gameObject.SetActive(false);
                playerInventory.addItem(this);
            }
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
}
