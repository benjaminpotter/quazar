using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public GameObject hud;
    public Button buildToggle;

    bool isHidden;

    void Start()
    {
        BuildManager bm = GetComponent<BuildManager>();
        buildToggle.onClick.AddListener(bm.ToggleBuildMode);
    }

    public void HideHUD (bool hide)
    {
        if (hide && !isHidden)
        {
            // hide HUD
            hud.SetActive(false);

            isHidden = true;
            Debug.Log("HUD Hidden");
        }

        else if (!hide && isHidden)
        {
            // show HUD
            hud.SetActive(true);

            Debug.Log("HUD Shown");
            isHidden = false;
        }
    }
}
