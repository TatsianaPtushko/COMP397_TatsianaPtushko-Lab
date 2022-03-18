using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerDataScriptableObject", menuName ="Scriptable Objects")]
public class PlayerDataScriptableObject : ScriptableObject
{
    private string m_playerID = "123456";

    public string PlayerID
    {
        get
        {
            return m_playerID;
        }
    }
    public string name;
   public int playerhealth;
    public Vector3 position;
    public Quaternion rotation;

}
