using UnityEngine;

// this class will have networked components
public class Planet : MonoBehaviour
{
    [HideInInspector]
    public PlanetController controller;

    public Transform model;

    void Start()
    {
        controller = GetComponent<PlanetController>();
    }

    // place a building on this planet
    public void Place()
    {
        // save building here
    }
}
