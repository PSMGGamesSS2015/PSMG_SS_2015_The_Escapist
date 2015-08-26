using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OutroGUIText : MonoBehaviour
{

    private Text txtRef;

    private string text_0 = "";

    private string text_1 = "Endlich, endlich fand Gerlin heraus, was passiert war.";
    private string text_2 = "Sie sollte Teil eines Experiments werden...";
    private string text_3 = "... Teil eines abscheulichen Experiments der Ritter des Gnarlekh' Clan.";
    private string text_4 = "Je mehr sie in dem Buch las, desto mehr verstand sie, dass es nur eine Sache zu tun gab:";
    private string text_5 = "So schnell es ging zu verschwinden!";
    private string text_6 = "Sie sah durch die langsam aufschwingenden Tore das Licht der Freiheit.";
    private string text_7 = "Sie blickte sich um. Sie schien allein zu sein.";
    private string text_8 = "Einen Schritt nach dem anderen schlich sie durch die Kirche.";
    private string text_9 = "Warm und weich war das Licht auf ihrer Haut, das durch die Tore fiel.";
    private string text_10 = "Hoffnung! Gab es wirklich Hoffnung, diesem Wahnsinn zu entkommen?";
    private string text_11 = "Es schien niemand sie bemerkt zu haben.";
    private string text_12 = "Zum Greifen nahe, endlich entkommen!";
    private string text_13 = "Oder?";

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
        yield return new WaitForSeconds(6.0f);
        txtRef.material.color = Color.black;
        txtRef.text = text_13;
        yield return new WaitForSeconds (6.0f);
        txtRef.text = text_0;
        yield return new WaitForSeconds(4.5f);


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
