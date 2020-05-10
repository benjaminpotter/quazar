using UnityEngine;

// deals with all user interaction?
public class UserInput : MonoBehaviour
{
    Vector3 start;
    Planet planet;

    BuildManager buildManager;
    CameraController camController;
    HUDManager hud;

    void Start()
    {
        planet = FindObjectOfType<Planet>();
        buildManager = GetComponent<BuildManager>();
        camController = GetComponent<CameraController>();
        hud = GetComponent<HUDManager>();
    }

    void Update()
    {
        // planet rotation 

        if (Input.GetMouseButtonDown(2))
            start = Input.mousePosition;

        if (Input.GetMouseButton(2)) {

            hud.HideHUD(true);

            Vector3 mouse = Input.mousePosition;

            if (start == Vector3.zero)
                start = mouse;

            // if the mouse moved this frame (while middle mouse is down) rotate the planet
            if (start != mouse) {  
                planet.controller.Rotate(start, mouse);
                start = mouse;
            }
        }

        // second mouse button not pressed unhide hud
        else
        {
            hud.HideHUD(false);
        }

        // building placement

        if (Input.GetMouseButtonDown(0))
        {
            buildManager.Build(planet);
        }

        float rotation = Input.GetAxisRaw("Horizontal");
        buildManager.RotateBuild(rotation);

        // camera zoom

        float scroll = Input.mouseScrollDelta.y;
        camController.Zoom(scroll);

        
    }
}
