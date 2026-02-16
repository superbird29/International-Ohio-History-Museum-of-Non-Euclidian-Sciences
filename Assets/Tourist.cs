using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourist : MonoBehaviour
{
    bool playerInRange;

    [SerializeField] Canvas displayPrompt;

    void Start()
    {
        playerInRange = false;
        displayPrompt.gameObject.SetActive(false);
    }

    void Update()
    {

    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collison Detected");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Collison Detected");
            playerInRange = true;
            displayPrompt.gameObject.SetActive(true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            displayPrompt.gameObject.SetActive(false);
        }
    }
}
