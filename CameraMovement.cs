using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CameraMovement : MonoBehaviour
{// sensors foro the camera and rotation 
    public float sensX = 100f;
    public float sensY = 100f;

    public Transform orientation;

    float xRotation = 0f;
    float yRotation = 0f;

    private void Start()
    {
        // locks the cursor to the center of the screen and makes it not visible 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    private void Update()
    {
        // ye ye simple movement u get the idea 
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
