using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JobType
    {
        Gift,
        Ticket,
        Tour
    }

public class Job : ScriptableObject
{

    public float duration = 30f;

    public JobManager jobManager;

    public LocalJobManager localJobManager;
    private float timeRemaining;
    private bool enabled = false;

    public JobType jobType;

    public void Tick(float deltaTime)
    {
        if(!enabled) return;

        timeRemaining -= deltaTime;
        if(timeRemaining <= 0f)
        {
            enabled = false;
            jobManager.FailJob(this);
            localJobManager.FailJob(this);
        }
    }

    public void CompleteJob()
    {
        enabled = false;
        jobManager.CompleteJob(this);
    }

    public void StartJob()
    {
        enabled = true;
        timeRemaining = duration;
    }

    private void OnEnable()
    {        enabled = false; 
        timeRemaining = duration;
    }
}
