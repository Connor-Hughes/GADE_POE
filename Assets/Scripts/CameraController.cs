using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;

    public float rSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Input.GetAxis("Horizontal") * speed;
        transform.position += transform.forward * Input.GetAxis("Vertical") * speed;

        transform.Rotate(Vector3.up, Input.GetAxis("Rotation") * rSpeed, Space.World);

    }
}
