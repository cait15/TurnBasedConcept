using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject gameUi;
    public GameObject Hero1;
    public GameObject Hero2;
    
    
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject PauseMenuu;

    private bool gameisPaused;
    public Slider vol;
    public AudioMixer MasterVol;
    
    public Slider volsFX;
 
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeVol()
    {
        MasterVol.SetFloat("Master", vol.value);
    }
    
    public void ChangeSFXVol()
    {
        MasterVol.SetFloat("SFX", volsFX.value);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameisPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    void Pause()
    {
        
        gameUi.SetActive(false);
        Enemy1.SetActive(false);
        Enemy2.SetActive(false);
        Enemy3.SetActive(false);

        Hero1.SetActive(false);
        Hero2.SetActive(false);
        gameisPaused = true;
        PauseMenuu.SetActive(true);
    }
    
    void Resume()
    {
        gameUi.SetActive(true);
        Enemy1.SetActive(true);
        Enemy2.SetActive(true);
        Enemy3.SetActive(true);

        Hero1.SetActive(true);
        Hero2.SetActive(true);
        gameisPaused = false;
        PauseMenuu.SetActive(false);
    }

}
