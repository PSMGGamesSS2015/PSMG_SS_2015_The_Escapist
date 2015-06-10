using UnityEngine;
using System.Collections;

public class HeadBobber : MonoBehaviour {

    private float timer = 0.0f;
    private float bobbingSpeed = 0.15f;
    private float bobbingAmount = 0.1f;
    private float midpoint = 1.464f;
    private bool isSneakingActive = false;
    private bool isRunningActive = false;
    private bool headBobAllowed = true;
    GamingControl control;


	// Use this for initialization
	void Start () {
        control = gameObject.AddComponent<GamingControl>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        checkMovementMode();
        if (headBobAllowed == true)
        {
            float waveslice = 0.0f;
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 cSharpConversion = transform.localPosition;

            if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
            {
                timer = 0.0f;
            }
            else
            {
                waveslice = Mathf.Sin(timer);
                timer = timer + bobbingSpeed;
                if (timer > Mathf.PI * 2)
                {
                    timer = timer - (Mathf.PI * 2);
                }
            }

            if (waveslice != 0)
            {
                float translateChange = waveslice * bobbingAmount;
                float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
                totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
                translateChange = totalAxes * translateChange;
                cSharpConversion.y = midpoint + translateChange;
            }
            else
            {
                cSharpConversion.y = midpoint;
            }

            transform.localPosition = cSharpConversion;
        }
	    
    }

    private void checkMovementMode()
    {
        if (control.isPlayerGrounded() == true)
        {
            headBobAllowed = true;
            isSneakingActive = control.isSneakingActive();
            isRunningActive = control.isRunningActive();

            if ((isSneakingActive == false) && (isRunningActive == false))
            {
                bobbingSpeed = 0.3f;
                bobbingAmount = 0.05f;
            }

            if (isSneakingActive == true)
            {
                bobbingSpeed = 0.15f;
                bobbingAmount = 0.08f;
            }

            if (isRunningActive == true)
            {
                bobbingSpeed = 0.35f;
                bobbingAmount = 0.08f;
            }
        }
        else
        {
            headBobAllowed = false;
        }
    }
  

}
