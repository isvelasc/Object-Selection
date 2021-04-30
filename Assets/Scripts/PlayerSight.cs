using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSight : MonoBehaviour
{

    [SerializeField] private float pitchSensitivity;
    [SerializeField] private float rollSensitivity;

    float mouseX;
    float mouseY;
    float LHRotation; // Local Horizontal
    float LVRotation; // Local Vertical
    //float dampener = 0.01f; // Optional

    [SerializeField] Transform FPCam;
    [SerializeField] Transform playerOrientation;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        SightInput();
    }

    private void SightInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        LVRotation += mouseX * rollSensitivity; // * dampener
        LHRotation -= mouseY * pitchSensitivity; // * dampener

        LHRotation = Mathf.Clamp(LHRotation, -90.0f, 90.0f);

        FPCam.transform.rotation = Quaternion.Euler(LHRotation, LVRotation, 0);
        playerOrientation.transform.rotation = Quaternion.Euler(0, LVRotation, 0);
    }
}
