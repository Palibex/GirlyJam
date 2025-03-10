using UnityEngine;
using TMPro;

public class Lettre : MonoBehaviour
{
    public CameraPhases phaseScript;
    public bool zoom = false;
    [Range (0,10)]
    public int consigneID;

    public string textIntro;
    public bool intro = true;
    public string[] consignes;
    public Vector3[] targetFactors;
    public Vector3 currentTargetFactor;

    public TextMeshPro tmpText; 
    public GameObject bigLettre;
    public Button bigLettreButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        phaseScript = GameObject.Find("Main Camera").GetComponent<CameraPhases>();
        zoom = true;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(zoom)
        {
            bigLettre.SetActive(true);
            bigLettreButton.active = true;
        }
        else
        {
            bigLettre.SetActive(false);
            bigLettreButton.active = false;
        }

        //Update Text and Objective

        if (intro)
        {
            tmpText.text = textIntro;
        }

        else
        {
            tmpText.text = consignes[consigneID];
            currentTargetFactor = targetFactors[consigneID];
    
            
        }

        //OutLine

        
        Material mat = GetComponent<Renderer>().material;
        float sineValue = Mathf.Sin(Time.time *3) * 0.05f;
        
        
        if (!zoom && phaseScript.phase != 3)
        {
            mat.SetFloat("_OutlineThickness", 0.9f  + sineValue);
        }
        else
        {
            mat.SetFloat("_OutlineThickness", 1);
        }


    }

    

    void OnMouseDown()
    {
        // Calculate the offset between object and mouse position
        zoom = true;
    }
}
