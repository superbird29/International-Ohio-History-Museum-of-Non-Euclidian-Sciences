using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourDeliveryZone : MonoBehaviour
{
    public Tourist tourist {get; set;}

    public Job job {get; set;}

    void Start()
    {
        tourist = null;
        job = null;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.Equals(tourist))
        {
            job.jobManager.CompleteJob(job);
            tourist.gameObject.SetActive(false);
        }
    }
}
