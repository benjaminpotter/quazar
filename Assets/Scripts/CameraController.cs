using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float minZoom;

    // zooms the camera
    public void Zoom(float direction)
    {
        Camera.main.fieldOfView += direction;

        if (Camera.main.fieldOfView > minZoom)
        {
            // Camera.main.fieldOfView = maxZoom;

            // zoom out of planet view into solar view
            GameManager.instance.LoadStarSystem();
        }
    }
}
