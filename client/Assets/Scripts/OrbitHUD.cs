using UnityEngine;

public class OrbitHUD : MonoBehaviour
{
    public int resolution; // number of nodes
    public float lineWidth;

    public bool isMousedOver;

    // saved to be able to spawn in planet
    // added by the star system view hud script
    public PlanetData planetData;

    float radius;
    public void SetRadius(float radius) { this.radius = radius; }

    LineRenderer path;

    void Start()
    {
        path = GetComponentInChildren<LineRenderer>();

        Vector3[] points = CalculateLine();
        DrawCircle(points, lineWidth);
    }

    static Vector3 CalculatePoint(float angle)
    {
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);

        return new Vector3(x, 0, y);
    }

    Vector3[] CalculateLine()
    {
        float radiansBetweenNode = Mathf.PI * 2 / resolution;
        float currentRadians = 0;

        Vector3[] positions = new Vector3[resolution + 1];

        for (int i = 0; i < resolution; i++)
        {
            positions[i] = CalculatePoint(currentRadians);
            currentRadians += radiansBetweenNode;
        }

        positions[positions.Length - 1] = positions[0];

        return positions;
    }

    void DrawCircle(Vector3[] line, float width)
    {
        path.widthMultiplier = width;
        path.positionCount = line.Length;
        path.SetPositions(line);
    }

    public bool CheckPoint(Vector3 point)
    {
        float distToCentre = point.magnitude; // assumes centre is 0,0,0
        return (distToCentre < (radius + lineWidth)) && (distToCentre > (radius - lineWidth));
    }

    public void OnSelected()
    {
        Color selColour = new Color(255, 255, 0);
        path.startColor = selColour;
        path.endColor = selColour;

        path.widthMultiplier = lineWidth + 1;
    }

    public void OnDeselected()
    {
        
    }

    public void OnMouseOver()
    {
        Color selColour = new Color(255, 255, 0);
        path.startColor = selColour;
        path.endColor = selColour;

        path.widthMultiplier = lineWidth + 1;

        // display a tool tip

    }

    public void OnMouseExit()
    {
        Color delColour = Color.white;
        path.startColor = delColour;
        path.endColor = delColour;

        path.widthMultiplier = lineWidth;
    }

    public void OnMouseDown()
    {

    }
}

