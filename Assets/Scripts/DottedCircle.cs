using UnityEngine;

public class DottedCircle : MonoBehaviour
{
    public int segments = 100; // Number of segments in the circle
    public float radius = 5f; // Radius of the circle
    public float gap = 0.2f; // Gap between dots
    public Material lineMaterial; // Material for the LineRenderer

    void Update()
    {
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments;
        lineRenderer.useWorldSpace = false;
        lineRenderer.loop = true;
        lineRenderer.material = lineMaterial;

        Vector3[] points = new Vector3[segments];
        for (int i = 0; i < segments; i++)
        {
            // Calculate angle for this segment
            float angle = 2 * Mathf.PI * i / segments;

            // Calculate position of the point
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            // Create gaps by making some points overlap
            if (i % 2 == 0)
                points[i] = new Vector3(x, y, 0);
            else
                points[i] = points[i - 1];
        }

        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }
}
