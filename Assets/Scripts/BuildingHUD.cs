using UnityEngine;
using UnityEngine.UI;

public class BuildingHUD : MonoBehaviour
{
    public GameObject buildingHUDElement;
    Transform page;

    void Start()
    {
        page = GetComponentInChildren<HorizontalLayoutGroup>().transform;
    }

    public void DrawHUD(GameObject[] buildingPrefabs)
    {
        for (int i = 0; i < buildingPrefabs.Length; i++)
        {
            GameObject inst = Instantiate(buildingHUDElement, page);
        }
    }
}
