using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkPlayerBehaviour : NetworkBehaviour
{
    public float speed;
    public MeshRenderer meshRenderer;

    private NetworkVariable<float> verticalPosition = new NetworkVariable<float>();
    private NetworkVariable<float> horizontalPosition = new NetworkVariable<float>();
    private NetworkVariable<Color> materialColor = new NetworkVariable<Color>();

    private float localHorizontal;
    private float localVertictal;
    private Color localColor;

    // Start is called before the first frame update

    private void Awake()
    {
        materialColor.OnValueChanged += ColorOnChange;
    
     if (IsClient){ 
            GetComponent<MeshRenderer>().material.color = materialColor.Value;
     }
    }

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        RandomSpawnPositionAndColor();

        meshRenderer.material.SetColor("_Color", materialColor.Value);
     
    }

    // Update is called once per frame
    void Update()
    {
        if (IsServer)
        {
            ServerUpdate();
        }
        if (IsClient && IsOwner)
        {
           ClientUpdate();
        }
    }

    void ServerUpdate()
    {
        transform.position = new Vector3(transform.position.x + horizontalPosition.Value,
            transform.position.y, transform.position.z +verticalPosition.Value);
   
       if(meshRenderer.material.GetColor("_Color") != materialColor.Value)
        {
            meshRenderer.material.SetColor("_Color",materialColor.Value);
        }
    
    }

    public void RandomSpawnPositionAndColor()
    {
        var x = Random.Range(-10.0f, 10.0f);
        var z = Random.Range(-10.0f, 10.0f);
        transform.position = new Vector3(x, 1.0f, z);

        var r = Random.Range(0, 1.0f);
        var g = Random.Range(0, 1.0f);
        var b = Random.Range(0, 1.0f);
       var color = new Color(r, g, b);
        localColor = color;
        
      //  SetClientColorServerRpc(color);
    }

    public void ClientUpdate()
    {
        var horiz = Input.GetAxis("Horizontal") * Time.deltaTime*speed;
        var vert = Input.GetAxis("Vertical") * Time.deltaTime*speed;
        
        //network update
       if(localHorizontal !=horiz || localVertictal != vert)
        {
            localHorizontal = horiz;
            localVertictal = vert;
            //update client position on the network
            UpdateClientPositionServerRpc(horiz, vert);
        }

       if(localColor != materialColor.Value)
        {
            SetClientColorServerRpc(localColor);
        }

    }

    [ServerRpc]
    public void UpdateClientPositionServerRpc(float horiz, float vert)
    {
        horizontalPosition.Value = horiz;
        verticalPosition.Value = vert;
    }

    [ServerRpc]
    public void SetClientColorServerRpc(Color color)
    {
        materialColor.Value = color;
        meshRenderer.material.SetColor("_Color", materialColor.Value);
       
    }

    void ColorOnChange(Color oldColor, Color newColor)
    {
        GetComponent<MeshRenderer>().material.color = materialColor.Value;
    }

}
