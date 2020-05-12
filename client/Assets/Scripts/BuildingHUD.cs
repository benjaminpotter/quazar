using UnityEngine;
using UnityEngine.UI;

public class BuildingHUD : MonoBehaviour
{
    public GameObject buildingHUDElement;
    public GameObject pagePrefab;

    GameObject[] pages;
    int activePage;

    // generate the hud object
    public void CreateHUD(BuildManager bm, GameObject[] buildingPrefabs)
    {
        pages = new GameObject[Mathf.CeilToInt(buildingPrefabs.Length / 10.0f)];

        for (int i = 0; i < pages.Length; i++) { 
            GameObject page = Instantiate(pagePrefab, transform);

            for (int j = 0; j < buildingPrefabs.Length; j++)
            {
                GameObject inst = Instantiate(buildingHUDElement, page.transform);
                inst.GetComponent<BuildingHUDElement>().OnSelection(bm, buildingPrefabs[j]);
                Debug.Log(buildingPrefabs[j]);
            }

            pages[i] = page;
            page.SetActive(false);
        }
    }

    // REFACTOR

    public void SetHUDState(bool active)
    {
        pages[activePage].SetActive(active);
    }
}
