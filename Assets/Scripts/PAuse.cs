using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAuse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }
    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }
}
