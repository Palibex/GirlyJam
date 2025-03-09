using UnityEngine;

public class Fiole : MonoBehaviour
{
    public Material glassMat;  // Assign the material you want to copy from
    public Material liquiMat;  // Assign the material you want to copy to
    
    
    public CameraPhases phaseScript;
    public bool active;
    public float targetHeight;
    public Material solaireMat;
    public Material stellaireMat;
    public Material lunaireMat;
    public string[] propertiesReferences;
    public Vector3 categoryFactor;

    public bool debug;


    void Update()
    {
        if (glassMat != null && liquiMat != null)
        {
            liquiMat.CopyPropertiesFromMaterial(glassMat);
        }
        else
        {
            Debug.LogError("Please assign both source and target materials.");
        }

        

        
        
        if (!debug)
        {
            
            glassMat.SetVector("_CategoryFactor", categoryFactor);


            if (phaseScript.phase == 3)
            {
                active = true;
            }
            else
            {
                active = false;
            }
            
            if (active)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0f, targetHeight,9), Time.deltaTime * 2);
            }
            else
            {
                transform.localPosition = new Vector3(0f, -15f,9);
            }
        }

        foreach (string reference in propertiesReferences)
        {
            AdjustProperty(reference);
        }

    }

    void AdjustProperty(string propertyReference)
    {
        //get values from mat
        float solValue = solaireMat.GetFloat(propertyReference) * categoryFactor.x;
        float stelValue = stellaireMat.GetFloat(propertyReference) * categoryFactor.y;
        float lunValue = lunaireMat.GetFloat(propertyReference) * categoryFactor.z;
        
        float sum = solValue + stelValue + lunValue;
        glassMat.SetFloat(propertyReference, sum);    

        
    }
}