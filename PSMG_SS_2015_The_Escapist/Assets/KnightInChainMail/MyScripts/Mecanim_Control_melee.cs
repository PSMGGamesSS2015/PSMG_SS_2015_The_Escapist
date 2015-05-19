using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mecanim_Control_melee : MonoBehaviour {
	public Animator animator;
	public bool leftMouseClick=false;
	public bool rightMouseClick=false;
	public bool canControl=true;
	private float shift_axis_late;
	public float leftMouseClicks;

	private float animLayer2;
	public float inputX;
	public float inputY;
	public float inputJump;


	

	void Start () {
		animator = GetComponent<Animator>();

	}
	
	void OnAnimatorIK(){
		animator.SetLayerWeight(1, 1f);
		animator.SetLayerWeight(2, animLayer2);
		
		if(canControl){
			Vector3 camDir =  transform.position - Camera.main.transform.position;
			Vector3 lookPos = transform.position + camDir;
			lookPos.y = transform.position.y -(Camera.main.transform.position.y - transform.position.y) + 10f;
			//animator.SetLookAtWeight(0.2f, 0.2f, 0.8f, 0.99f);
			//animator.SetLookAtPosition(lookPos);

		}
		
		
	
	}
	
	void Update () {
	
		if(leftMouseClick){
			StartCoroutine("TimerClickTime");

		}
		
		if(animator){	
			
			shift_axis_late = Mathf.Clamp((shift_axis_late - 0.005f), 0.0f, 1.1f);
			animLayer2 = Mathf.Clamp((animLayer2 - 0.01f), 0.0f, 1.0f);
			
			animator.SetBool("LeftMouseClick", leftMouseClick);
			
			animator.SetFloat("LeftShift_axis", shift_axis_late);
			animator.SetFloat("Axis_Horizontal", inputX);
			animator.SetFloat("Axis_Vertical", inputY);
			animator.SetFloat("Jump_axis", inputJump);
			animator.SetBool("RightMouse", rightMouseClick);

		}
		
		
		if(canControl){
					
		
			inputX = Input.GetAxis("Horizontal");
			inputY = Input.GetAxis("Vertical");
			inputJump = Input.GetAxis("Jump");
			leftMouseClick = Input.GetMouseButtonDown(0);
		
			
	
				
		if(Input.GetKeyDown(KeyCode.LeftShift)){
			shift_axis_late += 0.25f;
			
		}
		
	
		if(Input.GetAxis("Fire2")>0){
				rightMouseClick=true;
				animLayer2=0.5f;
		}
			else{
				rightMouseClick=false;
			}	
		


		//sync animator Y_axis rotations with Main Camera	
		if(inputX+inputY!=0){
			Vector3 camDir =  transform.position - Camera.main.transform.position;
			Vector3 lookPos = transform.position + camDir;
			lookPos.y = transform.position.y;
			transform.LookAt(lookPos);
		}

		}
		
	}
		



	void FightCombo(){   //every left mouse click +1 to animation number counter

		leftMouseClicks += 1f;
		animator.SetFloat("LeftMouseClicks", leftMouseClicks);

		if(leftMouseClicks>2f){
			leftMouseClicks = 0f;
		}

	}	

	IEnumerator TimerClickTime(){  //timer, few seconds after click mouse bool leftMouseClick = true
	
		yield return new WaitForSeconds(0.1f);
		leftMouseClick=false;
		yield return null;
	
	}
	
	
	IEnumerator InAction(){ //recieve message from fight animation in mecanim controller
		
		yield return null;
	}
	
	
	IEnumerator AnimationEnd(){//recieve message from fight animation in mecanim controller
		
		yield return null;
	}


}//The END
