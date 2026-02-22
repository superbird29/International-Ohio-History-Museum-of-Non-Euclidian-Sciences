using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftShopWaitingLine : MonoBehaviour
{
    [SerializeField] List<GameObject> waitingLineQueuers;

   private int nextInLine = -1;

    void Start()
    {
        waitingLineQueuers.ForEach(queuers => queuers.SetActive(false));
    }

    public void DisableNextCustomer()
    {
        if(nextInLine > waitingLineQueuers.Count -1 || nextInLine < 0) nextInLine = 0;
        waitingLineQueuers[nextInLine].SetActive(false);
        nextInLine++;
    }

    public void EnableNextCustomer()
    {
        Debug.Log("Enabling Customer");
        int startingPoint = nextInLine + 1;
        if(startingPoint > waitingLineQueuers.Count -1) startingPoint = 0;
        int currentPoint = startingPoint;
        do
        {
            Debug.Log(currentPoint);
            if (!waitingLineQueuers[currentPoint].activeInHierarchy)
            {
                Debug.Log("Enabling Customer at slot " + currentPoint);
                waitingLineQueuers[currentPoint].SetActive(true);
                break;
            }
            currentPoint++;
            if(currentPoint > waitingLineQueuers.Count -1) currentPoint = 0;
        } while(currentPoint != startingPoint);
    }
}
