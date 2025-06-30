using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{

    public float MouseSensitivity = 500f;
    float xRotation = 0f;
    float yRotation = 0f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;


        xRotation -= MouseY;
        xRotation = Math.Clamp(xRotation, -90f, 90f);

        yRotation += MouseX;
        
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);



    }
}
