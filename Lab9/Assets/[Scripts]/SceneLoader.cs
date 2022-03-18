using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        Debug.Log("Before next Scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       
    }
}
