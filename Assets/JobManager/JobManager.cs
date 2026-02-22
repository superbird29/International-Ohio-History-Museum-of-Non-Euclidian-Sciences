using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    [SerializeField] LocalJobManager ticketCounterJobManager;
    [SerializeField] LocalJobManager giftShopJobManager;
    [SerializeField] LocalJobManager tourGuidesJobManager;

    [SerializeField] List<JobType> startingJobWeights;

    [SerializeField] float jobLength = 60f;

    [SerializeField] float minGiftEarnings;
    [SerializeField] float maxGiftEarnings;

    [SerializeField] float minTicketEarnings;
    [SerializeField] float maxTicketEarnings;

    [SerializeField] float minTourEarnings;
    [SerializeField] float maxTourEarnings;

    private readonly List<JobType> lastTenJobs = new();

    private bool spawningJob;

    public List<Job> jobQueue = new();

    public float timeBetweenJobs = 60f;

    public int maxJobQueue = 5;

    private float timeUntilNextJob = 0f;
    // Start is called before the first frame update
    void Start()
    {
        timeUntilNextJob = 15f;
        lastTenJobs.AddRange(startingJobWeights.GetRange(0,10));
        jobQueue.ForEach(job => job.StartJob());
        spawningJob = false;
    }

    // Update is called once per frame
    void Update()
    {
        jobQueue.ForEach(job => job.Tick(Time.deltaTime));
        timeUntilNextJob = timeUntilNextJob - Time.deltaTime;
        if(timeUntilNextJob <= 0f && !spawningJob)
        {
            spawningJob = true;
            SpawnJob();
            timeUntilNextJob = timeBetweenJobs;
            spawningJob = false;
        }
    }
    
    public List<Job> GetJobQueue()
    {
        return jobQueue;
    }

    public void FailJob(Job failedJob)
    {
        timeBetweenJobs++;
        RemoveJob(failedJob);
    }

    public void CompleteJob(Job completedJob)
    {
        float moneyEarned = 0f;
        switch (completedJob.jobType)
        {
            case JobType.Gift:
            {
                moneyEarned = Random.Range(minGiftEarnings, maxGiftEarnings);
                break;
            }
            case JobType.Ticket:
            {
                moneyEarned = Random.Range(minTicketEarnings, maxTicketEarnings);
                break;
            }
            case JobType.Tour:
            {
                moneyEarned = Random.Range(minTourEarnings, maxTourEarnings);
                break;
            }
        }
        GameManager.Instance._rentManager.AddMoney(moneyEarned);
        RemoveJob(completedJob);
    }

    private void SpawnJob()
    {
        if(jobQueue.Count() >= maxJobQueue) return;

        Job newJob = new()
        {
            duration = jobLength,
            jobType = RollForJobType()
        };
        AddJob(newJob);
        UpdateLastTenJobTypes(newJob.jobType);
    }

    private void UpdateLastTenJobTypes(JobType jobType)
    {
        lastTenJobs.Remove(0);
        lastTenJobs.Add(jobType);
    }

    private JobType RollForJobType()
    {
        int result = Random.Range(0, 10);
        return lastTenJobs[result] == JobType.Gift ? JobType.Ticket : JobType.Gift;
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
                //tourGuidesJobManager.AddJob(addedJob);
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
