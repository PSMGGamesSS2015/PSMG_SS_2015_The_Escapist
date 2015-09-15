using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

    public Texture hurtEffect;
    private int counter = 0;
    private AudioSource audioSrc;
    private GamingControl gamingControl;
    private GameObject player;

    private GUIStyle style;

    public Font myFont;

    public bool displayHurtEffect = false;
    public bool displayDead = false;

    /// <summary>
    /// Check if enemy hit player
    /// </summary>
    /// 

    void Start()
    {
        style = new GUIStyle();
        style.fontSize = 100;
        style.normal.textColor = Color.white;
        style.font = myFont;
        style.alignment = TextAnchor.MiddleCenter;

        player = GameObject.FindGameObjectWithTag("Player");
        audioSrc = GetComponent<AudioSource>();
        gamingControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GamingControl>();
    }
    
    void OnTriggerEnter(Collider coll)
    {
        float distance = getDistanceTo(coll.gameObject.transform.position);

        if (coll.tag == "Enemy" && distance < 1.5f)
        {
            if (counter == 2)
            {
                displayDead = true;
                StartCoroutine(WaitForRestart());
            }
            else
            {
                audioSrc.Play();
                displayHurtEffect = true;

                counter++;
            }
        }
    }

    /// <summary>
    /// GUI Method for drawing HurtEffect
    /// </summary>
    void OnGUI()
    {
        if (displayHurtEffect)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), hurtEffect, ScaleMode.StretchToFill);
            StartCoroutine(StopDisplayingEffect());
        }

        if (displayDead)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), hurtEffect, ScaleMode.StretchToFill);
            GUI.Label(new Rect(Screen.width/2, Screen.height/2, 120, 120), "Du bist deinen Verletzungen erlegen!", style);
        }
    }

    /// <summary>
    /// Method for displaying the HurtEffect for a specific time
    /// </summary>
    IEnumerator StopDisplayingEffect()
    {
        yield return new WaitForSeconds(Constants.DISPLAY_TIME);
        counter = 0;
        displayHurtEffect = false;
    }

    IEnumerator WaitForRestart()
    {
        yield return new WaitForSeconds(3.0f);
        Application.LoadLevel(Application.loadedLevel);
    }

    private float getDistanceTo(Vector3 targetPos)
    {
        Vector3 direction = targetPos - transform.position;
        return direction.magnitude;
    }

    public void jumpDead()
    {
        displayDead = true;
        StartCoroutine(WaitForRestart());
    }
}
