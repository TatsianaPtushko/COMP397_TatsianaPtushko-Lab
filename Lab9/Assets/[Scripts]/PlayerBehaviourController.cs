using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourController : MonoBehaviour
{
    public CharacterController controller;
    public GameObject gameController;
  

    [Header("Movement")]
    public float maxSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 3.0f;
    public Vector3 velocity;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundRadius=0.1f;
    public LayerMask groundMask;
    public bool isGrounded;

    [Header("Health System")]
    public UIControls controls;
    public bool isColliding =false;

    [Header("Onscreen Joystick")]
    public Joystick leftJoystick;

    public GameObject onScreenControls;
    public GameObject miniMap;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                //turn OnScreen controls on
                onScreenControls.SetActive(true);
                break;
            case RuntimePlatform.WebGLPlayer:
            case RuntimePlatform.WindowsEditor:
                //turn OnScreen controls off
                onScreenControls.SetActive(false);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if(isGrounded && velocity.y < 0.0f)
        {
            velocity.y = -2.0f;
        }

        //keyboard input                      + //onscreen Joystick
        float x = Input.GetAxis("Horizontal") + leftJoystick.Horizontal;
        float z = Input.GetAxis("Vertical")+leftJoystick.Vertical;

        

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * maxSpeed * Time.deltaTime);

        if(Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        if (Input.GetKeyDown(KeyCode.M)) 
        {
            //Toggle minimap
            miniMap.SetActive(!miniMap.activeInHierarchy);
        }

            velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    public void OnJumpButtonPressed()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
    }

    public void OnMapButtonPressed()
    {//Toggle minimap
        miniMap.SetActive(!miniMap.activeInHierarchy);
    }
}
