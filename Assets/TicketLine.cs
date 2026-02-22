using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TicketLine : MonoBehaviour
{
   [SerializeField] List<GameObject> ticketLineQueuers;

   private int currentActiveQueuers;

    void Start()
    {
        currentActiveQueuers = ticketLineQueuers.Count;
        UpdateLineLength(0);
    }

    public void UpdateLineLength(int length)
    {
        if(length == currentActiveQueuers) return;
        currentActiveQueuers = length;

        ticketLineQueuers.ForEach(queuers => queuers.SetActive(false));
        for(int i = 0; i < length; i++)
        {
            ticketLineQueuers[i].SetActive(true);
        }
    }
}
