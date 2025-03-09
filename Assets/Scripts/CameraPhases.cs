using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraPhases : MonoBehaviour
{
    [Range(1, 3)]
    public int phase = 1;
    public int score = 0;
    public float posX1 = 5f;
    public float posX2 = 10f;
    public float posX3 = 15f;
    private Vector3 startPos;
    public float speed = 2f;
    public Button slotsButton;
    public Button caldronButton;
    public Button potionYesButton;
    public Button potionNoButton;



    public Cuillé cuilléScript;
    public Lettre lettreScript;
    public Fiole fioleScript;

    //Collections

    public GameObject[] ingredients;
    public GameObject[] slots;

    public Vector3 averageCategoryFactor;
    public Vector3 debugFactor;
    public Vector3 victoryMargin;
    public TextMeshPro scoreText; 
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        ingredients = GameObject.FindGameObjectsWithTag("Ingredient");
        slots = GameObject.FindGameObjectsWithTag("Slot");
    }

    // Update is called once per frame
    void Update()
    {
        //CameraMovement
        
        float targetX = 0;

        if (phase == 1)
        {
            targetX = posX1;
        }
        if (phase == 2)
        {
            targetX = posX2;
        }
        if (phase == 3)
        {
            targetX = posX3;
        }
        
        Vector3 targetPos = new Vector3(targetX, startPos.y, startPos.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, speed*Time.deltaTime);


        


        //Detect Full Slots
        int fullCount = 0;
        
        foreach (GameObject slot in slots)
        {
            if(slot.GetComponent<Slot>().full)
            {
                fullCount += 1;
            }
        }

        if (fullCount >= 5)
        {
            slotsButton.active = true;
        }
        else
        {
            slotsButton.active = false;
        }


        //Detect Full Caldron

        int inCaldronCount = 0;
        foreach (GameObject ingredient in ingredients)
        {
            if(ingredient.GetComponent<Ingredient>().inCaldron)
            {
                inCaldronCount += 1;
            }
        }

        if (inCaldronCount >= 0 == cuilléScript.progression > 100f)
        {
            caldronButton.active = true;
        }
        else
        {
            caldronButton.active = false;
        }

        //Average Category Factor
        
        averageCategoryFactor = Vector3.zero;
        foreach (GameObject ingredient in ingredients)
        {
            if (ingredient.GetComponent<Ingredient>().inCaldron)
            {
                averageCategoryFactor += ingredient.GetComponent<Ingredient>().catergoryFactor;
            }   
        }
        
        if (inCaldronCount > 0)
        {
            averageCategoryFactor /= inCaldronCount;
        }

        if (averageCategoryFactor == Vector3.zero)
        {
            averageCategoryFactor = debugFactor;
        }

        fioleScript.categoryFactor = averageCategoryFactor;









        //Detect Potion Phase
        if (phase == 3)
        {
            potionYesButton.active = true;
            potionNoButton.active = true;
        }
        else
        {
            potionYesButton.active = false;
            potionNoButton.active = false;
        }


    }

    public void CheckWin()
    {
        Vector3 minFactor = lettreScript.currentTargetFactor-victoryMargin;
        Vector3 maxFactor =  lettreScript.currentTargetFactor+victoryMargin;

        if (averageCategoryFactor.x >= minFactor.x && averageCategoryFactor.x <= maxFactor.x &&
            averageCategoryFactor.y >= minFactor.y && averageCategoryFactor.y <= maxFactor.y &&
            averageCategoryFactor.z >= minFactor.z && averageCategoryFactor.z <= maxFactor.z)
        {
            Debug.Log("Victoire");
            score += 1;
        }
        else
        {
            Debug.Log("Défaite");
            score = 0;
        }
    }

    public void Reset()
    {
        CheckWin();

        scoreText.text = score.ToString();
        
        cuilléScript.progression = 0;
        foreach ( GameObject ingredient in ingredients)
        {
            ingredient.transform.position = ingredient.GetComponent<Ingredient>().startPos;
            ingredient.GetComponent<Ingredient>().targetSlot = null;
            ingredient.GetComponent<Ingredient>().inCaldron = false;
        }
    }

    public void PotionDiscard()
    {
        cuilléScript.progression = 0;
        foreach ( GameObject ingredient in ingredients)
        {
            if (ingredient.GetComponent<Ingredient>().targetSlot != null)
            {
                ingredient.GetComponent<Ingredient>().inCaldron = false;
            }
            
        }
    }
}
