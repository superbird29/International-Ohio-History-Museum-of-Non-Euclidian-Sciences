using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TicketJobManager : LocalJobManager
{

    [SerializeField] CreditCardPad creditCardPad;

    [SerializeField] GameObject creditCardMinigameCanvas;
    public override void JobAdded(Job addedJob)
    {
        creditCardPad.BeginMinigame();
        return;
    }

    public override void JobCompleted(Job completedJob)
    {
        return;
        //Spawn Tourist
    }

    public override void JobFailed(Job failedJob)
    {
        creditCardMinigameCanvas.SetActive(false);
        if (JobInQueue())
        {
            creditCardPad.ForceStartMinigame();
        }
    }

    public void CompleteNextJob()
    {
        CompleteJob(jobQueue[0]);
    }
}