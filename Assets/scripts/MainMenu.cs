using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    public GameObject howToPlay;
    
  
    private void Start()
    {
        howToPlay.GameObject().SetActive(false);
       
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("design");
    }

    public void HowToPlay()
    {
        howToPlay.GameObject().SetActive(true);
    }
    
    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
