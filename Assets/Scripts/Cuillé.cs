using UnityEngine;

public class Cuillé : MonoBehaviour
{
      // Center of the allowed movement area
    public float range = 5f;   // Allowed radius
    public Vector3 offset;     // Offset from the center
    public float lerpSpeed = 2f;
    public Transform skin;
    public Vector3 center;


    public Vector3 lastPos;
    public float speed;
    public float progression;

    //Reset
    public 

    void Start()
    {
        center = transform.position;
    }
    
    void Update()
    {
        
        
        // Get current position (keeping the offset)
        Vector3 targetPosition = transform.position;

        // Constrain the position within the allowed circle
        transform.position = ConstrainToCircle(targetPosition, center, range) + offset;
        skin.position = Vector3.Lerp(skin.position, transform.position, Time.deltaTime * lerpSpeed);

        getSpeed();
        progression += speed;
        if (progression < 100f)
        {
            progression -= Time.deltaTime;
        }
        progression = Mathf.Clamp(progression, 0, 150);
    }

    private Vector3 ConstrainToCircle(Vector3 position, Vector3 center, float radius)
    {
        // Compute direction from the center
        Vector3 direction = position - center;
        float distance = direction.magnitude;

        // If outside the allowed radius, move it back within range
        if (distance > radius)
        {
            direction = direction.normalized * radius;
            return center + direction;
        }

        return position; // Stay in the same position if within range
    }

    void getSpeed()
    {
        float distance = Vector3.Distance(transform.position, lastPos);
        lastPos = transform.position;
        speed = distance;
    }
}