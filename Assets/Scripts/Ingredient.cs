using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public bool onSlot;
    public bool inCaldron;
    public Transform targetSlot;
    private DragAndDrop dragAndDropScript;
    private CameraPhases phaseScript;
    private GameObject caldron;
    public Vector3 startPos;
    public GameObject[] slots;
    public float distanceToSnap = 3f;
    
    
    public Vector3 catergoryFactor;

    private Vector3 startScale;
    

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dragAndDropScript = GetComponent<DragAndDrop>();
        startPos = transform.position;
        slots = GameObject.FindGameObjectsWithTag("Slot");
        phaseScript = GameObject.Find("Main Camera").GetComponent<CameraPhases>();
        caldron = GameObject.FindWithTag("Caldron");
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (phaseScript.phase == 1)
        {
            if (dragAndDropScript.isDragged == true)
            {
                targetSlot = null;
            }
            foreach (GameObject slot in slots)
            {
                float distance = Vector3.Distance(transform.position, slot.transform.position);
                if (distance < distanceToSnap && !slot.GetComponent<Slot>().full)
                {
                    targetSlot = slot.transform;
                }
            }
        }

        if (targetSlot != null && dragAndDropScript.isDragged == false)
        {
            transform.position = targetSlot.position;
            onSlot = true;
        }
        else
        {
            onSlot = false;
        }

        if (phaseScript.phase == 2)
        {
            float distance = Vector3.Distance(transform.position, caldron.transform.position);
            if (distance < distanceToSnap*3)
            {
                inCaldron = true;
            }
        }
        
        
        
        
        if (inCaldron && dragAndDropScript.isDragged == false)
        {
            transform.position = targetSlot.position;
            transform.GetChild(0).GetComponent<Renderer>().enabled = false;
        }
        else
        {
            transform.GetChild(0).GetComponent<Renderer>().enabled = true;
        }



        //Scale and yPos

        if (dragAndDropScript.isDragged)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, startScale *2.5f, Time.deltaTime*5);
            transform.GetChild(0).localPosition = Vector3.Lerp(transform.GetChild(0).localPosition, new Vector3(0f,0.5f,0f), Time.deltaTime*5);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, startScale, Time.deltaTime*5);
            transform.GetChild(0).localPosition = Vector3.Lerp(transform.GetChild(0).localPosition, Vector3.zero, Time.deltaTime*5);
        }


        //Outline Anim
        
        Material mat = transform.GetChild(0).gameObject.GetComponent<Renderer>().material;
        float sineValue = Mathf.Sin(Time.time *3) * 0.05f;
        
        if (dragAndDropScript.isDragged)
        {
            mat.SetFloat("_OutlineThickness", 0.9f);
        }
        else if (!inCaldron && !onSlot)
        {
            mat.SetFloat("_OutlineThickness", 0.9f  + sineValue);
        }
        else
        {
            mat.SetFloat("_OutlineThickness", 1);
        }
    }

    

    
}
