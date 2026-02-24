using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    public string SceneName;
    public AudioSource buttonSound;
    public void SwitchScene()
    {
        buttonSound.Play();
        SceneManager.LoadScene(SceneName);
    }

    public void QuitGame()
    {
        buttonSound.Play();
        Application.Quit();
    }
}
