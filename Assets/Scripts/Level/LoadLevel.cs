using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private string levelName;

    public void LoadScene() 
    {
        SceneManager.LoadScene(levelName);
    }
}
