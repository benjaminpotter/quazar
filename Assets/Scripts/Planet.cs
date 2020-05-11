using UnityEngine;

using System.Collections.Generic;

// this class will have networked components
public class Planet : MonoBehaviour
{
    [HideInInspector]
    public PlanetController controller;

    public Transform model;

    void Start()
    {
        controller = GetComponent<PlanetController>();
        GenerateModel();
    }

    [Range(0.01f, 0.2f)]
    public float strength;
    Vector3 AdjustVertex(Vector3 vertex)
    {
        float adjustment = Random.Range(1 - strength, 1 + strength);
        return vertex * adjustment;
    }

    Vector3[] AdjustMesh(Vector3[] currentVertices)
    {
        Vector3[] adjusted = new Vector3[currentVertices.Length];
        for (int i = 0; i < currentVertices.Length; i++)
        {
            adjusted[i] = AdjustVertex(currentVertices[i]);
        }

        return adjusted;
    }

    Vector3[] GetVertices(Mesh mesh)
    {
        List<Vector3> vertices = new List<Vector3>();
        mesh.GetVertices(vertices);

        return vertices.ToArray();
    }

    Vector3[] GetNormals(Mesh mesh)
    {
        List<Vector3> normals = new List<Vector3>();
        mesh.GetNormals(normals);

        return normals.ToArray();
    }

    public GameObject crater;
    void GenerateModel()
    {
        Mesh baseMesh = IOManager.LoadAsset("Meshes/basePlanet").GetComponent<MeshFilter>().sharedMesh;

        Vector3[] vertices = GetVertices(baseMesh);
        Vector3[] normals = GetNormals(baseMesh);

        int randomVertex = Random.Range(0, vertices.Length - 1);

        Instantiate(crater, vertices[randomVertex], Quaternion.Euler(normals[randomVertex]), transform);

        GetComponent<MeshFilter>().mesh.vertices = vertices;
        GetComponent<MeshFilter>().mesh = baseMesh;
        GetComponent<MeshCollider>().sharedMesh = baseMesh;
    }

    // place a building on this planet
    public void Place()
    {
        // save building here
    }
}
