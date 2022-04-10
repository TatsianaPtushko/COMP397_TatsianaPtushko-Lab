using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float controllerSensitivity = 2.0f;
    public Transform playerBody;
    public Joystick rightJoystick;

    private float xRotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput;
        float verticalInput;

        if (Application.platform != RuntimePlatform.Android)
        {
            horizontalInput = Input.GetAxis("Mouse X") * controllerSensitivity;
            verticalInput = Input.GetAxis("Mouse Y") * controllerSensitivity;
        }
        else
        {
             horizontalInput = rightJoystick.Horizontal;
             verticalInput = rightJoystick.Vertical;
        }

        xRotation -= verticalInput;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        playerBody.Rotate(Vector3.up * horizontalInput);
    }
}
