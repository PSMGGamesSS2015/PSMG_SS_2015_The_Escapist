using UnityEngine;
using System.Collections;

public class HurtEffect : MonoBehaviour
{

    public Texture hurtEffect;
    private int counter = 0;
    private AudioSource audioSrc;
    private GamingControl gamingControl;
    private GameObject enemy;

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

        enemy = GameObject.FindGameObjectWithTag("Enemy");
        audioSrc = GetComponent<AudioSource>();
        gamingControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GamingControl>();


    }
    void OnTriggerEnter(Collider col)
    {
        float distance = Vector3.Distance(enemy.transform.position, gamingControl.getPlayerPosition());
        if (col.gameObject == enemy && distance < 1.5)
        {
            if (counter == 2)
            {
                displayDead = true;
                StartCoroutine(WaitForRestart());
            }
            else
            {
                counter++;
                audioSrc.Play();

                displayHurtEffect = true;
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
}
