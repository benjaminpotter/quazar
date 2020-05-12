using UnityEngine;

public class StarSystemViewHUD : MonoBehaviour
{
    public GameObject orbitHUDPrefab;
    public GameObject planetHUDPrefab;

    public GameObject planetToolTip;

    OrbitHUD[] orbitHUDs;

    public void GenerateStarSystemHUD(PlanetData[] planets)
    {
        orbitHUDs = new OrbitHUD[planets.Length];

        for (int i = 0; i < orbitHUDs.Length; i++)
        {
            PlanetData planet = planets[i];

            // generate the paths
            GameObject instantiatedHUD = Instantiate(orbitHUDPrefab);

            int orbitRadius = planet.orbitRadius;
            instantiatedHUD.transform.localScale = new Vector3(orbitRadius, orbitRadius, orbitRadius);

            orbitHUDs[i] = instantiatedHUD.GetComponent<OrbitHUD>();
            orbitHUDs[i].SetRadius(orbitRadius);
            orbitHUDs[i].planetData = planet;

            // generate the planet objects
            GameObject instantiatedPlanetHUD = Instantiate(planetHUDPrefab);

            // set the planet size
            float planetRadius = planet.GetRadius();
            instantiatedPlanetHUD.transform.localScale = new Vector3(planetRadius, planetRadius, planetRadius);

            // set the orbit height
            instantiatedPlanetHUD.GetComponent<Orbit>().orbitRadius = planet.orbitRadius;
        }
    }

    void UpdateHUD(Vector3 point, bool mouseClick)
    {
        foreach (var orbit in orbitHUDs)
        {
            bool isMousedOver = orbit.CheckPoint(point);

            // could break here

            if (mouseClick) { 
                orbit.OnMouseDown();
                GameManager.instance.LoadPlanet(orbit.planetData);
            }

            // if the mouse event happened this frame
            if (orbit.isMousedOver != isMousedOver)
            {
                orbit.isMousedOver = isMousedOver;

                // if the mouse entered the orbit
                if (orbit.isMousedOver)
                {
                    orbit.OnMouseOver();
                    // display tool tip?

                    //Instantiate(planetToolTip, FindObjectOfType<Canvas>().transform);
                }

                // if the mouse left the orbit
                else
                {
                    orbit.OnMouseExit();
                }
            }
        }
    }

    void Update()
    {
        // planet selection

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag != "Listener")
            {
                return;
            }

            UpdateHUD(hit.point, Input.GetMouseButtonDown(0));
        }
    }
}
