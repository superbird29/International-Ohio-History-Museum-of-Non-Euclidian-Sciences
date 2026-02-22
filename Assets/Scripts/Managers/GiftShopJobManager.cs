using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftShopJobManager : LocalJobManager
{
    [SerializeField] GiftShopWaitingLine giftShopWaitingLine;
    public override void JobAdded(Job addedJob)
    {
        giftShopWaitingLine.EnableNextCustomer();
    }

    public override void JobCompleted(Job completedJob)
    {
        giftShopWaitingLine.DisableNextCustomer();
    }

    public override void JobFailed(Job failedJob)
    {
        giftShopWaitingLine.DisableNextCustomer();
    }

    public void CompleteNextJob()
    {
        JobCompleted(jobQueue[0]);
    }
}
