using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftShopWaitingLine : MonoBehaviour
{
    [SerializeField] List<GameObject> waitingLineQueuers;

   private int nextInLine = -1;
   private int lastEnabledCustomer = -1;

    void Start()
    {
        waitingLineQueuers.ForEach(queuers => queuers.SetActive(false));
    }

    public void DisableNextCustomer()
    {
        if(nextInLine > waitingLineQueuers.Count -1 || nextInLine < 0) nextInLine = 0;
        Debug.Log("Disabling Customer in slot " + nextInLine);
        waitingLineQueuers[nextInLine].SetActive(false);
        nextInLine++;
    }

    public void EnableNextCustomer()
    {
        Debug.Log("Enabling Customer");

        int enablingCustomer = lastEnabledCustomer + 1;
        if(enablingCustomer > waitingLineQueuers.Count -1) enablingCustomer = 0;

        Debug.Log("Enabling customer in slot " + enablingCustomer);
        waitingLineQueuers[enablingCustomer].SetActive(true);

        lastEnabledCustomer = enablingCustomer;
    }
}
