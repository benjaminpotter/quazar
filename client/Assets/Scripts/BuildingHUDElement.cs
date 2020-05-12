using UnityEngine;
using UnityEngine.UI;

public class BuildingHUDElement : MonoBehaviour
{

    public void OnSelection(BuildManager bm, GameObject building)
    {
        GetComponent<Button>().onClick.AddListener(() => bm.SetActiveBuildPrefab(building));
    }
}
