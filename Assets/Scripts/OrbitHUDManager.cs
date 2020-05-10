using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

struct Torus2D
{
    public Vector2[] outside;
    public Vector2[] inside;
}

public class OrbitHUDManager : MonoBehaviour
{
    public int resolution; // number of nodes
    public float lineWidth;

    float radius;
    public void SetRadius(float radius) { this.radius = radius; }

    LineRenderer path;
    PolygonCollider2D col;

    void Start()
    {
        path = GetComponentInChildren<LineRenderer>();
        col = GetComponent<PolygonCollider2D>();

        Vector3[] points = CalculateLine();
        Torus2D lineCol = CalculateCollider(points, lineWidth);
        col.pathCount = 2;
        col.SetPath(0, lineCol.outside);
        col.SetPath(1, lineCol.inside);
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

    static Torus2D CalculateCollider(Vector3[] nodes, float width)
    {
        Torus2D torusCollider = new Torus2D();
        torusCollider.outside = new Vector2[nodes.Length];
        torusCollider.inside = new Vector2[nodes.Length];

        for (int i = 0; i < nodes.Length; i++)
        {
            Vector3 nodePosition = nodes[i];

            Vector2 directionFromCentre = new Vector2(nodePosition.x, nodePosition.z); // assumes the center is 0, 0, 0

            torusCollider.outside[i] = new Vector2(nodePosition.x, nodePosition.z) - directionFromCentre;
            torusCollider.inside[i] = new Vector2(nodePosition.x + width, nodePosition.z) - directionFromCentre;
        }

        return torusCollider;
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

    public void Selected()
    {
        Color selColour = new Color(255, 255, 0);
        path.startColor = selColour;
        path.endColor = selColour;

        path.widthMultiplier = lineWidth + 1;
    }

    public void Deselected()
    {
        Color delColour = Color.white;
        path.startColor = delColour;
        path.endColor = delColour;

        path.widthMultiplier = lineWidth;
    }

    public void OnMouseEnter()
    {
        Debug.Log("Hovered");
    }
}
