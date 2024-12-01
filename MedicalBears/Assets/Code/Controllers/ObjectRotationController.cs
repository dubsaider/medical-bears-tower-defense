using UnityEngine;

public class ObjectRotationController : MonoBehaviour
{
    public float speed = 10f;

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            //Vector3 mouse = new Vector3(Input.GetAxis("Mouse X") * Time.deltaTime, 0, 0);
            Quaternion rotationY = Quaternion.AngleAxis(1 * speed, new Vector3(0, Input.GetAxis("Mouse X") * -1, 0));
            transform.rotation *= rotationY;
        }
    }
}
