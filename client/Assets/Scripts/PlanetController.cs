using UnityEngine;

// responsible for the movement of the planet in the physical world
// should not be networked
public class PlanetController : MonoBehaviour 
{
    public float spinSpeed;

    Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // should the maths happen inside the controller?
    public void Rotate(Vector3 start, Vector3 end)
    {
        Vector3 input = start - end;

        float degrees = input.magnitude * spinSpeed;

        input.Normalize();
        Vector3 axis = new Vector3(-input.y, input.x, 0); // perpendicular
        Quaternion rotation = Quaternion.AngleAxis(degrees, axis);

        transform.Rotate(rotation.eulerAngles, Space.World);
    }
}
