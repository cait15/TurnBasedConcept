using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUiHandler : MonoBehaviour
{
    
    
    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // reloads the scene
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}
