using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class JobQueueDisplay : MonoBehaviour
{
    [SerializeField] JobManager jobManager;

    [SerializeField] JobDisplayCard jobDisplayCardPrefab;

    [SerializeField] LayoutGroup displayRow;

    public List<Job> jobQueue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateJobDisplayRow();
    }

    public void UpdateJobDisplayRow()
    {
        List<Job> jobsNoLongerInQueue = new();
        jobsNoLongerInQueue.AddRange(jobQueue);
        jobsNoLongerInQueue.RemoveAll(job => jobManager.GetJobQueue().Contains(job));

        List<Job> newJobsInQueue = new();
        newJobsInQueue.AddRange(jobManager.GetJobQueue());
        newJobsInQueue.RemoveAll(job => jobQueue.Contains(job));

        if(jobsNoLongerInQueue.Count == 0 && newJobsInQueue.Count == 0) return;

        jobQueue = new();
        jobQueue.AddRange(jobManager.GetJobQueue());
        
        int childCount = displayRow.transform.childCount;
    for (int i = childCount - 1; i >= 0; i--)
    {
        GameObject.Destroy(transform.GetChild(i).gameObject);
    }
        jobQueue.Reverse();
         jobQueue.ForEach(job => AddJobToDisplayRow(job));
    }

    private void AddJobToDisplayRow(Job newJob)
    {
        JobDisplayCard newJobDisplayCard = Instantiate(jobDisplayCardPrefab,transform);
        newJobDisplayCard.job = newJob;
    }
}
