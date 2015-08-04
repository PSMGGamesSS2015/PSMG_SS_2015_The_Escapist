using UnityEngine;
using System.Collections;
using System;

public class ShadowDetection : MonoBehaviour
{
    public GameObject openeye;
    public GameObject closedeye;

    public float brightnessApprox; // http://stackoverflow.com/questions/596216/formula-to-determine-brightness-of-rgb-color 
    public float brightness; // http://www.nbdtech.com/Blog/archive/2008/04/27/Calculating-the-Perceived-brightness-of-a-Color.aspx
    public float hiddenPercentage;
    public Color avgSurfaceColor;
    public LayerMask layerMask;
    public Texture debugMapMarker;
    public float probes = 25;
    public float detectionRadius = 20;
    public float alphaFactor = 5;

    private Texture2D lightmapTex;
    private Vector2 pixelUV;
    private bool debugMode = false;



    void Update()
    {
        Raycast();
        calcDetectionValues();

        if (Input.GetKeyUp(KeyCode.U)) { debugMode = !debugMode; }
    }

    void calcDetectionValues()
    {
        brightnessApprox = (avgSurfaceColor.r + avgSurfaceColor.r + avgSurfaceColor.b + avgSurfaceColor.g + avgSurfaceColor.g + avgSurfaceColor.g) / 6;
        brightness = Mathf.Sqrt((avgSurfaceColor.r * avgSurfaceColor.r * 0.2126f + avgSurfaceColor.g * avgSurfaceColor.g * 0.7152f + avgSurfaceColor.b * avgSurfaceColor.b * 0.0722f));
        hiddenPercentage = Math.Max(0, 100 - (brightness * (avgSurfaceColor.a * 4)) * 100);
    }

    void OnGUI()
    {
        bool safe = (hiddenPercentage > 70) ? true : false;

        GUI.Label(new Rect(20, 80, 120, 256), "Versteckt: " + (int)(hiddenPercentage) + "%");

        if (!safe)
        {
            openeye.SetActive(true);
            closedeye.SetActive(false);
        }
        else
        {
            closedeye.SetActive(true);
            openeye.SetActive(false);
        }
    }

    void Raycast()
    {
        // RAY TO PLAYER'S FEET
        Ray ray = new Ray(transform.position + new Vector3(0, 0.3f, 0), -Vector3.up);
        Debug.DrawRay(ray.origin, ray.direction * 5f, Color.magenta);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 5f, layerMask))
        {
            // GET RENDERER OF OBJECT HIT
            Renderer hitRenderer = hitInfo.collider.GetComponent<Renderer>();
            //Debug.Log(LightmapSettings.lightmaps.Length + " " + hitRenderer.lightmapIndex);

            // GET LIGHTMAP APPLIED TO OBJECT
            LightmapData lightmapData = LightmapSettings.lightmaps[hitRenderer.lightmapIndex];

            // STORE LIGHTMAP TEXTURE
            Texture2D lightmapTex = lightmapData.lightmapFar;
            this.lightmapTex = lightmapTex;

            // GET LIGHTMAP COORDINATE WHERE RAYCAST HITS
            Vector2 pixelUV = hitInfo.lightmapCoord;
            this.pixelUV = pixelUV;
            //Debug.Log("x: " + pixelUV.x * 1024 + " y: " + pixelUV.y * 1024);

            // CALC AVERAGE COLOR AROUND THE LIGHTMAP COORDINATE
            this.avgSurfaceColor = calcAverageColor(pixelUV);
        }
    }

    Color calcAverageColor(Vector2 pixelUV)
    {
        int xNorm = (int)(pixelUV.x * 1024);
        int yNorm = (int)(pixelUV.y * 1024);
        //Debug.Log("xNorm: " + xNorm + " yNorm: " + yNorm);

        float r = 0;
        float g = 0;
        float b = 0;
        float a = 0;

        float steps = detectionRadius * 2 / (float)(Math.Sqrt(probes));

        Debug.Log("steps: " + steps);
        Debug.Log("detectionRadius: " + detectionRadius);

        int actualProbes = 0;

        for (float x = xNorm - detectionRadius; x < (xNorm + detectionRadius) - 0.1; x += steps)
        {
            for (float y = yNorm - detectionRadius; y < (yNorm + detectionRadius) - 0.1; y += steps)
            {
                Color avgSurfaceColor = lightmapTex.GetPixelBilinear(x / 1024f, y / 1024f);
                r += avgSurfaceColor.r;
                g += avgSurfaceColor.g;
                b += avgSurfaceColor.b;
                a += avgSurfaceColor.a;
                actualProbes++;

                //Debug.Log("x: " + x/1024f + " y: " + y/1024f);
                //Debug.Log("x: " + x + " y: " + y);
            }
        }
        Debug.Log("actual probes: " + actualProbes);

        return new Color(r / probes, g / probes, b / probes, a / alphaFactor);
    }

    public int getHiddenPercentage()
    {
        return (int)(hiddenPercentage);
    }
}