using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;

    bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        gameOver = false;
    }

    public void GameOver()
    {
        if(gameOver) return;
        gameOver = true;
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }

    public void Restart()
    {
        Debug.Log("Restart");
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
