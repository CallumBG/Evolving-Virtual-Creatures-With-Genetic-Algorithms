using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu2 : MonoBehaviour
{
    public bool isMuted = false;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void MuteToggle()
    {
        if(isMuted)
        {
            isMuted = false;
            AudioManager.instance.sounds[0].source.volume = 1;
        }
        else
        {
            isMuted = true;
            AudioManager.instance.sounds[0].source.volume = 0;
        }
        
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
