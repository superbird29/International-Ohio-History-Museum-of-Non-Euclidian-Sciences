using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{

    public AudioSource buttonSound;
    public void SwitchScene()
    {
        buttonSound.Play();
        SceneManager.LoadScene("PandaScene");
    }

    public void QuitGame()
    {
        buttonSound.Play();
        Application.Quit();
    }
}
