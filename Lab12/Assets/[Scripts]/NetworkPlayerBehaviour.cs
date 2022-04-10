using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkPlayerBehaviour : NetworkBehaviour
{
   CharacterController controller;
    
    [Header("Movement")]
    public float maxSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 3.0f;
    public Vector3 velocity;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundRadius = 0.1f;
    public LayerMask groundMask;
    public bool isGrounded;




    private NetworkVariable<float> remoteVerticalInput = new NetworkVariable<float>();
    private NetworkVariable<float> remoteHorizontalInput = new NetworkVariable<float>();
    private NetworkVariable<bool> remoteJumpInput = new NetworkVariable<bool>();

    private float localHorizontalInput;
    private float localVertictalInput;
    private bool localJumpInput;

    // Start is called before the first frame update

    
    void Start()
    {
        if (!IsLocalPlayer)
        {
          GetComponentInChildren<NetworkCameraController>().enabled =false;
          GetComponentInChildren<Camera>().enabled = false;
        }
        else
        {
            controller = GetComponent<CharacterController>();
        }

        if (IsServer)
        {
        RandomSpawnPosition();
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (IsServer)
        {
            ServerUpdate();
        }

        if (IsOwner)
        {
           ClientUpdate();
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void LateUpdate()
    {
        if (IsLocalPlayer)
        {
            UpdateRotationYServerRPC(transform.eulerAngles.y);
        }
    }

    [ServerRpc]

    void UpdateRotationYServerRPC(float newRotationY) 
    {
        transform.rotation = Quaternion.Euler(0f, newRotationY, 0f);
    }

    private void Move()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if (isGrounded && velocity.y < 0.0f)
        {
            velocity.y = -2.0f;
        }

        Vector3 move = transform.right * remoteHorizontalInput.Value + transform.forward * remoteVerticalInput.Value;
        GetComponent<CharacterController>().Move(move * maxSpeed * Time.deltaTime);

        if (remoteJumpInput.Value && isGrounded )
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
              
       velocity.y += gravity * Time.deltaTime;
        GetComponent<CharacterController>().Move(velocity * Time.deltaTime);

    }

public void RandomSpawnPosition()
    {
        var x = Random.Range(-4.0f, 4.0f);
        var z = Random.Range(-4.0f, 4.0f);
        transform.position = new Vector3(x, 1.0f, z);

    }

    void ServerUpdate()
    {
        Move();
   
    }
       

    public void ClientUpdate()
    {
        //keyboard input  
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        bool isJumping = Input.GetButton("Jump");

        //check if local variables have changed
        if (localHorizontalInput != x || localVertictalInput != z || localJumpInput != isJumping)
        {
            localHorizontalInput = x;
            localVertictalInput = z;
            localJumpInput = isJumping;

            //update client position on the network
            UpdateClientPositionServerRpc(x, z, isJumping);
        }

    }

    [ServerRpc]
    public void UpdateClientPositionServerRpc(float horiz, float vert, bool isJumping)
    {
        remoteHorizontalInput.Value = horiz;
        remoteVerticalInput.Value = vert;
        remoteJumpInput.Value = isJumping;
    }


}
