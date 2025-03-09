using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    public bool isDragged;

    void Start()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        // Calculate the offset between object and mouse position
        offset = transform.position - GetMouseWorldPosition();
        isDragged = true;
    }

    void OnMouseUp()
    {
        isDragged = false;
    }

    void OnMouseDrag()
    {
        // Update the object position while dragging
        transform.position = GetMouseWorldPosition() + offset;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = cam.WorldToScreenPoint(transform.position).z; // Maintain Z-depth
        return cam.ScreenToWorldPoint(mousePoint);
    }
}
