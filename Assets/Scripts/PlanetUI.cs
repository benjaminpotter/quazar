using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetUI : MonoBehaviour
{
    public GameObject orbitPath;
    public GameObject star;

    public float mouseRange;

    Orbit planetOrbit;
    OrbitHUDManager hud;

    void Start()
    {
        planetOrbit = GetComponent<Orbit>();

        DrawOrbitUI();
    }

    public void CheckInput(Vector3 point)
    {
        float distToCentre = point.magnitude; // assumes centre is 0,0,0
        if (distToCentre < (planetOrbit.orbitRadius + mouseRange) && distToCentre > (planetOrbit.orbitRadius - mouseRange))
        {
            hud.Selected();
        }

        else
        {
            hud.Deselected();
        }
    }

    void DrawOrbitUI()
    {
        GameObject insUI = Instantiate(orbitPath);
        insUI.transform.localScale = new Vector3(planetOrbit.orbitRadius, planetOrbit.orbitRadius, planetOrbit.orbitRadius);
        hud = insUI.GetComponent<OrbitHUDManager>();
        hud.SetRadius(planetOrbit.orbitRadius);
    }
}
