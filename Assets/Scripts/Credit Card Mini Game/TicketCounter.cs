using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketCounter : MonoBehaviour
{
    bool playerInBooth;

    bool ccEntered;

    [SerializeField] TicketJobManager ticketJobManager;

    [SerializeField] Canvas creditCardPadCanvas;

    [SerializeField] CreditCardPad creditCardPad;

    [SerializeField] TicketLine ticketLine;

    void Start()
    {
        playerInBooth = false;
        ccEntered = false;
        creditCardPadCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerInBooth && Input.GetButtonDown("Interact") && ticketJobManager.JobInQueue())
        {
            creditCardPad.BeginMinigame();
            creditCardPadCanvas.gameObject.SetActive(true);
        }

        if (ccEntered)
        {
            ccEntered = false;
            ticketJobManager.CompleteNextJob();
        }
        ticketLine.UpdateLineLength(ticketJobManager.jobQueue.Count);
    }

    public void CCEntered()
    {
        ccEntered = true;
    }

    public void DisableCCPadCanvas()
    {
        creditCardPadCanvas.gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && ticketJobManager.JobInQueue())
        {
            playerInBooth = true;
            GameManager.Instance._player.ActiveInteractPopup();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInBooth = false;
            GameManager.Instance._player.DeactiveInteractPopup();
            creditCardPadCanvas.gameObject.SetActive(false);
        }
    }
}
