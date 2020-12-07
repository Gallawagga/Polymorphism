using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] float cameraSpeed = 20f;
    Camera attachedCamera;
    [SerializeField] int Y;
    [SerializeField] int X;

    private void Start()
    {
        attachedCamera = GetComponent<Camera>();
    }

    void Update()
    {
        CameraMovement();
    }

    void CameraMovement()
    {
        attachedCamera.transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * cameraSpeed, Input.GetAxisRaw("Vertical") * Time.deltaTime * cameraSpeed);
        attachedCamera.transform.position = new Vector3(Mathf.Clamp(transform.position.x, -X, X), Mathf.Clamp(transform.position.y, -Y, Y));
    }
}
