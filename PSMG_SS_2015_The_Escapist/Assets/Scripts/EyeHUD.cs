﻿using UnityEngine;
using System.Collections;

public class EyeHUD : MonoBehaviour {
    public GameObject openeye;
    public GameObject openeyered;

    private FollowAI fai;
    private Color color;


    private GamingControl gc; 
        // Use this for initialization
	void Start () {
        color = new Color(1f, 1f, 1f);

        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GamingControl>();
        fai = GameObject.FindWithTag("Enemy").GetComponent<FollowAI>();
    }

    void OnGUI()
    {
        bool safe = (gc.getPlayerHiddenPercentage() > 90) ? true : false;

        GUI.Label(new Rect(20, 80, 120, 256), "Versteckt: " + (int)(gc.getPlayerHiddenPercentage()) + "%");


        if (fai.isChasing() == true)
        {
            openeye.SetActive(false);
            openeyered.SetActive(true);

        }
        else if (safe && fai.isChasing() == false)
        {
            openeye.SetActive(true);
            openeyered.SetActive(false);
        }
        else if (!safe && fai.isChasing() == false)
        {
            openeye.SetActive(true);
            openeyered.SetActive(false);
        }
        openeye.GetComponent<CanvasRenderer>().SetColor(color);
        
    }
}
