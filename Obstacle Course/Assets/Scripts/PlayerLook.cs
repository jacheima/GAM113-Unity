using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private string mouseXInputName, mouseYInputName;
    public float mouseSensitivity = 100;
    float tempMouseSens;

    [SerializeField] private Transform playerBody;

    private float xAxisClamp;


    private void Awake()
    {
        LockCursor();
        xAxisClamp = 0.0f;
    }
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    private void Update()
    {
        CameraRotaiton();

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void FreezeCameraRotation()
    {
        if (mouseSensitivity != 0)
        {
            tempMouseSens = mouseSensitivity;
            mouseSensitivity = 0;
        }
    }

    public void UnfreezeCameraRotation()
    {
        mouseSensitivity = tempMouseSens;
    }
    private void CameraRotaiton()
    {

        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationValue(270.0f);
        }
        else if (xAxisClamp < -90.0)
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationValue(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void ClampXAxisRotationValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
