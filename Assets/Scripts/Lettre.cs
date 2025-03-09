using UnityEngine;
using TMPro;

public class Lettre : MonoBehaviour
{
    public CameraPhases phaseScript;
    public bool zoom = false;
    [Range (0,10)]
    public int consigneID;

    public string[] consignes;
    public Vector3[] targetFactors;
    public string currentText;
    public Vector3 currentTargetFactor;

    public TextMeshPro tmpText; 
    public GameObject bigLettre;
    public Button bigLettreButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        phaseScript = GameObject.Find("Main Camera").GetComponent<CameraPhases>();
        
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
        currentText = consignes[consigneID];
        currentTargetFactor = targetFactors[consigneID];

        tmpText.text = currentText;


    }

    

    void OnMouseDown()
    {
        // Calculate the offset between object and mouse position
        zoom = true;
    }
}
