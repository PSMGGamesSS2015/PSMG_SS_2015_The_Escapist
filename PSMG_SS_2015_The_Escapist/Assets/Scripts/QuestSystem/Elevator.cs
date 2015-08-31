using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

    public GameObject elevator;
    public GameObject coverPlattform;

    public AnimationClip thirdToSecondFloor;
    public AnimationClip secondToThirdFloor;
    public AnimationClip coverPlattformAnim;

    private Animation elevatorAnim;
    private Animation plattformAnim;

    public enum States { secondFloor, thirdFloor };
    public States state = States.thirdFloor;
	

    void Start()
    {
        elevatorAnim = elevator.GetComponent<Animation>();
        plattformAnim = coverPlattform.GetComponent<Animation>();
    }

    public void trigger()
    {
            if (state == States.thirdFloor)
            {
                plattformAnim.clip = coverPlattformAnim;
                plattformAnim.Play();

                elevatorAnim.clip = thirdToSecondFloor;
                elevatorAnim.Play();
                state = States.secondFloor;
            }
            else
            {
                elevatorAnim.clip = secondToThirdFloor;
                elevatorAnim.Play();

                state = States.thirdFloor;
            }
    }
}
