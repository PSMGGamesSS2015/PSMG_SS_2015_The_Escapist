using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OutroGUIText : MonoBehaviour
{

    private Text txtRef;

    private string text_0 = "";

    private string text_1 = "Gerlin finally found out why all of this happened.";
    private string text_2 = "She should have been part in an experiment...";
    private string text_3 = "... an insane experiment of the crazy knights from the Gnarlekh' Clan.";
    private string text_4 = "The more she read in the book the better she knew there is only thing to do:";
    private string text_5 = "To get the hell out of here!";
    private string text_6 = "She saw the opening doors flooded by divine light.";
    private string text_7 = "It seemed she was all alone.";
    private string text_8 = "Gerlin did one silent step after another.";
    private string text_9 = "A warm, smooth feeling came up on her skin as she saw the doors wide open.";
    private string text_10 = "Hope! There was finally hope to escape this madness!";
    private string text_11 = "Nobody recognized her - the way was free";
    private string text_12 = "Now go, Gerlin, go! You can finally ESCAPE !";

    private string credits_1 = "CREDITS:";
    private string credits_2 = "... developed by ...";

    private string credits_3 = "... Christoph Herbert ...";
    private string credits_4 = "... Julien Wachter ...";
    private string credits_5 = "... Tobias Zirngibl ...";
    private string credits_6 = "... Daniel Hecht ...";
    private string credits_7 = "... Oliver Poeppel ...";
    private string credits_8 = "... Benedikt Hierl ...";

    private string credits_9 = "Thank you for playing!";
    private string credits_10 = "... Game development @ Uni Regensburg in SS15 ...";
    private string credits_11 = "GAME OVER";




    // Use this for initialization
    void Start()
    {
        //References the Text-Object
        txtRef = GetComponent<Text>();
        StartCoroutine(SetGUIText());
    }



    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SetGUIText()
    {


        //Change text color to white
        txtRef.material.color = Color.white;

        //Display story texts
        txtRef.text = text_1;
        yield return new WaitForSeconds(5.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(2.0f);

        txtRef.text = text_2;
        yield return new WaitForSeconds(5.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(2.0f);

        txtRef.text = text_3;
        yield return new WaitForSeconds(5.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(2.0f);

        txtRef.text = text_4;
        yield return new WaitForSeconds(5.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(1.5f);

        txtRef.text = text_5;
        yield return new WaitForSeconds(7.5f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(2.0f);

        txtRef.text = text_6;
        yield return new WaitForSeconds(4.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(2.0f);

        txtRef.text = text_7;
        yield return new WaitForSeconds(8.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(6.0f);

        txtRef.text = text_8;
        yield return new WaitForSeconds(12.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(5.0f);

        txtRef.text = text_9;
        yield return new WaitForSeconds(7.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(2.0f);

        txtRef.text = text_10;
        yield return new WaitForSeconds(5.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(2.0f);

        txtRef.text = text_11;
        yield return new WaitForSeconds(3.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(1.0f);

        txtRef.text = text_12;
        yield return new WaitForSeconds(6.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(16.5f);


        //Display credits texts here

        //At first change the color to black to make it readable
        txtRef.material.color = Color.black;

        txtRef.text = credits_1;
        yield return new WaitForSeconds(4.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(1.0f);

        txtRef.text = credits_2;
        yield return new WaitForSeconds(4.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(1.0f);

        txtRef.text = credits_3;
        yield return new WaitForSeconds(4.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(1.0f);

        txtRef.text = credits_4;
        yield return new WaitForSeconds(4.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(1.0f);

        txtRef.text = credits_5;
        yield return new WaitForSeconds(4.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(1.0f);

        txtRef.text = credits_6;
        yield return new WaitForSeconds(4.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(1.0f);

        txtRef.text = credits_7;
        yield return new WaitForSeconds(4.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(1.0f);

        txtRef.text = credits_8;
        yield return new WaitForSeconds(4.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(1.0f);

        txtRef.text = credits_9;
        yield return new WaitForSeconds(3.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(1.0f);

        txtRef.text = credits_10;
        yield return new WaitForSeconds(3.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(6.8f);


        //Change the text color to red to make it look final
        txtRef.material.color = Color.red;

        txtRef.text = credits_11;
    }
}
