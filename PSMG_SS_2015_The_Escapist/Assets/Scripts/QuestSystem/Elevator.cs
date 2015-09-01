using UnityEngine;
using System.Collections;

public class Elevator : InteractiveObject {

    public GameObject elevator;
    public GameObject coverPlattform;
    public HighlightObject buttonHighlight;
    public HighlightObject leverHighlight;

    public AnimationClip thirdToSecondFloor;
    public AnimationClip secondToThirdFloor;
    public AnimationClip coverPlattformOpenAnim;
    public AnimationClip coverPlattformCloseAnim;

    private GameObject player;
    private Animation elevatorAnim;
    private Animation plattformAnim;

    private bool playerInside = false;
    private bool elevatorActive = false;

    public enum States { secondFloor, thirdFloor };
    public States state = States.thirdFloor;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        elevatorAnim = elevator.GetComponent<Animation>();
        plattformAnim = coverPlattform.GetComponent<Animation>();
    }

    public override void trigger()
    {
        if (elevatorActive) { return; }

        if (state == States.thirdFloor)
        {
            plattformAnim.clip = coverPlattformOpenAnim;
            elevatorAnim.clip = thirdToSecondFloor;

            startElevator();
        }
        else
        {
            plattformAnim.clip = coverPlattformCloseAnim;
            elevatorAnim.clip = secondToThirdFloor;

            startElevator();
        }
    }

    private void startElevator()
    {
        elevatorActive = true;
        leverHighlight.setActive(false);
        buttonHighlight.setActive(false);

        Transform originalPlayerParent = player.transform.parent;
        if (playerInside) { player.transform.parent = elevator.transform; }

        plattformAnim.Play();
        elevatorAnim.Play();

        StartCoroutine(WaitForAnimation(elevatorAnim, originalPlayerParent));
    }

    private IEnumerator WaitForAnimation(Animation anim, Transform originalParent)
    {
        do
        {
            yield return null;
        } while (anim.isPlaying);

        elevatorActive = false;
        leverHighlight.setActive(true);
        buttonHighlight.setActive(true);

        if (playerInside) { player.transform.parent = originalParent; }

        state = (state == States.thirdFloor) ? States.secondFloor : States.thirdFloor;
    }

    public void setPlayerInside(bool p)
    {
        playerInside = p;
    }

    public override int getState()
    {
        return (int)(state);
    }

    public override bool hasInteractions()
    {
        return true;
    }
}
