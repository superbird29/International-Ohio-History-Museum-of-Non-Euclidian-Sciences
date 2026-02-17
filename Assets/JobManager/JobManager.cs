using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    [SerializeField] LocalJobManager ticketCounterJobManager;
    [SerializeField] LocalJobManager giftShopJobManager;
    [SerializeField] LocalJobManager tourGuidesJobManager;

    public List<Job> jobQueue = new List<Job>();

    public float timeBetweenJobs = 60f;

    public int maxJobQueue = 5;

    private float timeUntilNextJob = 0f;
    // Start is called before the first frame update
    void Start()
    {
        timeUntilNextJob = timeBetweenJobs;
    }

    // Update is called once per frame
    void Update()
    {
        jobQueue.ForEach(job => job.Tick(Time.deltaTime));
        timeUntilNextJob = timeUntilNextJob - Time.deltaTime;
        if(timeUntilNextJob <= 0f && jobQueue.Count < 5)
        {
            //Spawn Job
            timeUntilNextJob = timeBetweenJobs;
        }
    }

    public void FailJob(Job failedJob)
    {
        //Talk to egg manager?
        RemoveJob(failedJob);
    }

    public void CompleteJob(Job completedJob)
    {
        //Talk to money manager
        RemoveJob(completedJob);
    }

    public void AddJob(Job addedJob)
    {
        jobQueue.Add(addedJob);
        addedJob.jobManager = this;
        switch (addedJob.jobType)
        {
            case JobType.Gift:
            {
                giftShopJobManager.AddJob(addedJob);
                break;
            }
            case JobType.Ticket:
            {
                ticketCounterJobManager.AddJob(addedJob);
                break;
            }
            case JobType.Tour:
            {
                tourGuidesJobManager.AddJob(addedJob);
                break;
            }
        }
        addedJob.StartJob();
    }

    private bool RemoveJob(Job removedJob)
    {
        return jobQueue.Remove(removedJob);
    }
}
