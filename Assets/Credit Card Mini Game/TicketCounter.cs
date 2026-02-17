using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketCounter : MonoBehaviour
{
    bool playerInBooth;

    bool ccEntered;

    [SerializeField] Canvas displayPrompt;

    [SerializeField] Canvas creditCardPadCanvas;

    [SerializeField] CreditCardPad creditCardPad;

    void Start()
    {
        playerInBooth = false;
        ccEntered = false;
        displayPrompt.gameObject.SetActive(false);
        creditCardPadCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerInBooth && Input.GetButtonDown("Interact"))
        {
            creditCardPad.BeginMinigame();
            creditCardPadCanvas.gameObject.SetActive(true);
        }

        if (ccEntered)
        {
            ccEntered = false;
            creditCardPadCanvas.gameObject.SetActive(false);
            //Spawn Tourist
            //Complete job
        }
    }

    public void CCEntered()
    {
        ccEntered = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInBooth = true;
            displayPrompt.gameObject.SetActive(true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInBooth = false;
            displayPrompt.gameObject.SetActive(false);
            creditCardPadCanvas.gameObject.SetActive(false);
        }
    }
}
