using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public float[] position;
    public float[] rotation;

   public PlayerData()
    {
        position = new float[3]; //instantiate an empty array container
        rotation = new float[3]; //instantiate an empty array container
    }
}