﻿using UnityEngine;
using System.Collections;

public class HeadBobber : MonoBehaviour {

    private float timer = 0.0f;
    float bobbingSpeed = 0.18f;
    float bobbingAmount = 0.2f;
    float midpoint = 1.464f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	float waveslice = 0.0f;
     float horizontal = Input.GetAxis("Horizontal");
     float vertical = Input.GetAxis("Vertical");
  
  Vector3 cSharpConversion = transform.localPosition; 
  
     if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0) {
        timer = 0.0f;
     }
     else {
        waveslice = Mathf.Sin(timer);
        timer = timer + bobbingSpeed;
        if (timer > Mathf.PI * 2) {
           timer = timer - (Mathf.PI * 2);
        }
     }
     if (waveslice != 0) {
        float translateChange = waveslice * bobbingAmount;
        float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
        totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
        translateChange = totalAxes * translateChange;
        cSharpConversion.y = midpoint + translateChange;
     }
     else {
        cSharpConversion.y = midpoint;
     }
  
  transform.localPosition = cSharpConversion;
  }
  

}
