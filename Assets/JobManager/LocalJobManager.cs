using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class LocalJobManager : MonoBehaviour
{
    public List<Job> jobQueue = new();

    //This should be called by whatever is managing the completion of jobs upon the completion of a job.
    public void CompleteJob(Job completedJob)
    {
        RemoveJob(completedJob);
        completedJob.CompleteJob();
        JobCompleted(completedJob);
    }

    //This method will be called by the job itself when time runs out.
    public void FailJob(Job failedJob)
    {
        RemoveJob(failedJob);
        JobFailed(failedJob);
    }

    //This is called by the job manager when adding a new job.
    public void AddJob(Job addedJob)
    {
        jobQueue.Add(addedJob);
        addedJob.localJobManager = this;
        JobAdded(addedJob);
    }

    //Implement this method as needed for when a job is added
    public abstract void JobAdded(Job addedJob);

    //Implement this method as needed for when a job is failed
    public abstract void JobFailed(Job failedJob);

    //Implement this method as needed for when a job is completed
    public abstract void JobCompleted(Job completedJob);

    private bool RemoveJob(Job removedJob)
    {
        return jobQueue.Remove(removedJob);
    }
}