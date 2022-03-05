using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaneController : MonoBehaviour
{
    //public Transform playerRespawn;
    public GameObject controller;

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

        //access the spawn position of current generated map
        MapGenerator script = controller.GetComponent<MapGenerator>();
        
        //set player position to spawn position from map ganerator script
        player.transform.position = script.sPoint.transform.position; //playerRespawn.position;
        player.GetComponent<CharacterController>().enabled = true;
    }
}
