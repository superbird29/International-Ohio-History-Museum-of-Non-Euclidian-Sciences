using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauser : MonoBehaviour
{
    public GameObject Mep;
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Mep.SetActive(true);
        }
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
