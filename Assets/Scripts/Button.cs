using UnityEngine;

public class Button : MonoBehaviour
{
    public bool active;
    public CameraPhases phaseScript;
    public int targetPhase;
    public bool reset;
    public bool potionDiscard;
    public bool closeLettre;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        phaseScript = GameObject.Find("Main Camera").GetComponent<CameraPhases>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            GetComponent<Renderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
            
        }
        else
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }

    void OnMouseDown()
    {
        // Calculate the offset between object and mouse position
        if (active)
        {
            if (!closeLettre)
            {
                phaseScript.phase = targetPhase;
            }
            active = false;

            if (reset)
            {
                phaseScript.Reset();
            }

            if (potionDiscard)
            {
                phaseScript.PotionDiscard();
                phaseScript.score -= 1;
            }

            if (closeLettre)
            {
                phaseScript.lettreScript.zoom = false;
            }
        }
    }
}
