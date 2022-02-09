using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaneController : MonoBehaviour
{
    public Transform playerRespawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player colliding with Death Plane");
            RespawnPlayer(other.gameObject);
        }
    }

    public void RespawnPlayer(GameObject player)
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = playerRespawn.position;
        player.GetComponent<CharacterController>().enabled = true;
    }
}
