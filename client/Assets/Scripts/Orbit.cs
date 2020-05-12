using UnityEngine;

public class Orbit : MonoBehaviour
{
    public float orbitRadius;
    public float orbitSpeed; // degrees per second
    public bool asymetric;

    Vector3 origin;
    float currentDegrees;

    void Start()
    {
        currentDegrees = Random.Range(90, 270);
    }

    void Update()
    {
        float desiredRot = currentDegrees;

        // cw is default
        if (!asymetric)
            desiredRot = -desiredRot;

        // unit length
        Vector3 target = new Vector3(Mathf.Cos(desiredRot), 0, Mathf.Sin(desiredRot));
        target *= orbitRadius;

        transform.position = target;

        currentDegrees += orbitSpeed * Time.deltaTime;
    }
}
