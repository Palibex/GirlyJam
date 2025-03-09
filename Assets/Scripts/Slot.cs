using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool full;
    public GameObject[] ingredients;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ingredients = GameObject.FindGameObjectsWithTag("Ingredient");
    }

    // Update is called once per frame
    void Update()
    {
        full = false;
        foreach (GameObject ingredient in ingredients)
        {
            if (ingredient.transform.position == transform.position)
            {
                full = true;
            }
        }
    }
}
