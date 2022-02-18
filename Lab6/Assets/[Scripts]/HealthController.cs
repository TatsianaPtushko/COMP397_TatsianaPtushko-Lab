using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    
    public UIControls controls;
    public int damagePower = 10;

    public void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.CompareTag("Player"))
        {
            controls.TakeDamage(damagePower);
        }
    }

}
