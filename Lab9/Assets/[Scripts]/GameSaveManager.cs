using System.Collections;
using System.Collections.Generic;
//for Binary formatter
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
//
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public Transform player;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
    }

    //Using PlayerPrefs
    /*
        void SaveGame()
    {
        string playerPosition = JsonUtility.ToJson(player.position);
        string playerRotation = JsonUtility.ToJson(player.rotation.eulerAngles);
        PlayerPrefs.SetString("PlayerPosition", playerPosition);
        PlayerPrefs.SetString("PlayerRotation", playerRotation);
        PlayerPrefs.Save();
        Debug.Log("Game data saved!");

    }

    void LoadGame()
    {
        if (PlayerPrefs.HasKey("PlayerPosition"))
        {
            player.gameObject.GetComponent<CharacterController>().enabled = false;
            player.position = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("PlayerPosition"));
            player.rotation = Quaternion.Euler(JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("PlayerRotation")));
            player.gameObject.GetComponent<CharacterController>().enabled = true;
            Debug.Log("Game data Loaded!");
        }
        else
        {
            Debug.LogError("There is no saved data!");
        }
    }

    void ResetData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Data reset complete");
    }
    */

    //using Binary formatter
   void  SaveGame() 
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath +"/PlayerData.ttt");
        PlayerData data = new PlayerData();
        data.position = new[]{player.position.x, player.position.y,player.position.z };
        data.rotation = new[] { player.rotation.eulerAngles.x, player.rotation.eulerAngles.y, player.rotation.eulerAngles.z };
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
        
    }

    void LoadGame()
    { 
        if(File.Exists(Application.persistentDataPath + "/PlayerData.ttt"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlayerData.ttt",FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            player.gameObject.GetComponent<CharacterController>().enabled = false;
            player.position = new Vector3(data.position[0], data.position[1], data.position[2]);
            player.rotation = Quaternion.Euler(data.rotation[0], data.rotation[1], data.rotation[2]);
            player.gameObject.GetComponent<CharacterController>().enabled = true;
            Debug.Log("Game data Loaded!");
            Debug.Log(JsonUtility.ToJson(data));
        }
        else
        {
            Debug.LogError("There is no saved data!");
        }

    }

    void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.ttt"))
        {
            File.Delete(Application.persistentDataPath + "/PlayerData.ttt");
            Debug.Log("Data reset complete");
        }
        else
        {
            Debug.Log("No saved data to delete.");
        }
    }
        //end of Binary formatter

        public void OnSaveButtonPressed()
    {
        SaveGame();
    }

    public void OnLoadButtonPressed()
    {
        LoadGame();
    }
}
