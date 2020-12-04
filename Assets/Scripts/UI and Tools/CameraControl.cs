using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    Camera attachedCamera;
    [SerializeField]
    float cameraSpeed = 20f;
    
    private Vector2 moveVelocity;

    // Start is called before the first frame update
    void Start()
    {
        attachedCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
    }

    void CameraMovement()
    {
        //so the code here basically states when the 'horizontal' binded keys are pressed the input is equal to -1 (left) and 1 (right).
        //ditto for the 'vertical input, they're pushing the player component up or down, left or right.
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * cameraSpeed;
        //MyPlayer.Move(moveInput);
    }
}
