using UnityEngine;

public class LineToMouse : MonoBehaviour
{
    public LineRenderer lineRenderer;

    void Start()
    {
        // Add or get the LineRenderer component
        if (lineRenderer == null)
            return;
           // lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Configure LineRenderer properties
        lineRenderer.startWidth = 0.1f; // Line width at start
        lineRenderer.endWidth = 0.1f;   // Line width at end
        lineRenderer.positionCount = 2; // Line with two points
        lineRenderer.useWorldSpace = true;
    }

    void Update()
    {
        if(gameObject.GetComponent<Boomerang>().isMoving)
        {
            lineRenderer.gameObject.SetActive(false);
        }
        else
        {
            lineRenderer.gameObject.SetActive(true);
        }

        if(lineRenderer.gameObject.activeSelf == false) return;
        // Set the starting position to the GameObject's position
        lineRenderer.SetPosition(0, transform.position);

        // Get the mouse position in world space
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));

        // Ensure the Z position matches your world (especially for 2D)
        mouseWorldPosition.z = transform.position.z;

        // Set the end position to the mouse position
        lineRenderer.SetPosition(1, mouseWorldPosition);
    }
}
