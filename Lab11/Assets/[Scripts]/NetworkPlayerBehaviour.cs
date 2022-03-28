using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerBehaviour : NetworkBehaviour
{
    public float spead;
    private NetworkVariable<float> verticalPosition = new NetworkVariable<float>();
    private NetworkVariable<float> horizontalPosition = new NetworkVariable<float>();

    private float localHorizontal;
    private float localVertictal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsServer)
        {
            //server update
        }
        if (IsClient && IsOwner)
        {
            //client update
            ClientUpdate();
        }
    }

    void ServerUpdate()
    {
        transform.position = new Vector3(transform.position.x + horizontalPosition.Value,
            transform.position.y, transform.position.z +verticalPosition.value);
    }

    public void RandomSpawnPosition()
    {
        var x = Random.Range(-10f, 10f);
        var z = Random.Range(-10f, 10f);
        transform.position = new Vector3(x, 1.0f, z);

    }

    public void ClientUpdate()
    {
        var horiz = Input.GetAxis("Horizontal") * Time.deltaTime*spead;
        var vert = Input.GetAxis("Vertical") * Time.deltaTime*spead;
        
        //network update
       if(localHorizontal !=horiz || localVertictal != vert)
        {
            localHorizontal = horiz;
            localVertictal = vert;

            //update client position on the network
            UpdateClientPositionServerRpc(horiz, vert);
        }
    }

    [ServerRpc]
    public void UpdateClientPositionServerRpc(float horiz, float vert)
    {
        horizontalPosition.Value = horiz;
        verticalPosition.Value = vert;
    }

}
