using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public BuildingHUD buildingHUD;

    public GameObject buildingPrefab;
    public GameObject guide;
    GameObject insGuide;

    GameObject[] buildingPrefabs;

    bool isBuilding;
    bool isValidBuild;

    void Start()
    {
        buildingPrefabs = LoadBuildingPrefabs();
        buildingHUD.CreateHUD(this, buildingPrefabs);
    }

    public void ToggleBuildMode()
    {
        isBuilding = !isBuilding;

        if (isBuilding) { 
            SpawnGuide();
            buildingHUD.SetHUDState(true);
        } else
        {
            DestroyGuide();
            buildingHUD.SetHUDState(false);
        }
    }

    static GameObject[] LoadBuildingPrefabs()
    {
        // loads all building prefabs
        Object[] objects = Resources.LoadAll("Buildings");
        GameObject[] gos = new GameObject[objects.Length];
        for (int i = 0; i < objects.Length; i++)
        {
            gos[i] = (GameObject) objects[i];
        }

        return gos;
    }

    public GameObject[] GetBuildingPrefabs()
    {
        return buildingPrefabs;
    }

    void SpawnGuide()
    {
        insGuide = Instantiate(guide, transform);
    }

    void DestroyGuide()
    {
        Destroy(insGuide);
    }

    void DrawGuide(Vector3 position, Vector3 normal)
    {
        if (position == Vector3.zero && normal == Vector3.zero)
        {
            Debug.Log("Invalid guide position");
            return;
        }

        insGuide.transform.position = position;
        insGuide.transform.rotation = Quaternion.LookRotation(normal);
    }


    // problamo with the way the building prefabs are set up
    public void RotateBuild(float rotation)
    {
        if (isBuilding)
        {
            insGuide.transform.Rotate(insGuide.transform.up, rotation);
        }
    }

    public void Build(Planet surface)
    {
        if (isBuilding && isValidBuild) { 
            GameObject insBuilding = Instantiate(buildingPrefab, surface.transform);

            insBuilding.transform.position = insGuide.transform.position;
            insBuilding.transform.rotation = insGuide.transform.rotation;

            surface.Place();
        }
    }

    // called from the building hud
    public void SetActiveBuildPrefab(GameObject prefab)
    {
        buildingPrefab = prefab;
        Debug.Log("Set building prefab");
    }

    void Update()
    {
        if (!isBuilding)
        {
            return;
        }

        Vector3 position = Vector3.zero;
        Vector3 normal = Vector3.zero;

        // passive raycast for model outline placement
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (!hit.collider.GetComponent<Planet>())
            {
                return;
            }

            position = hit.point;
            normal = hit.normal;

            isValidBuild = true;
        } else { isValidBuild = false; }

        DrawGuide(position, normal);
    }
}
