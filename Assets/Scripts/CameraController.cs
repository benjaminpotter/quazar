using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float maxZoom;

    // zooms the camera
    public void Zoom(float direction)
    {
        Camera.main.fieldOfView += direction;

        if (Camera.main.fieldOfView > maxZoom)
        {
            // Camera.main.fieldOfView = maxZoom;

            // zoom out of planet view into solar view
        }
    }
}
