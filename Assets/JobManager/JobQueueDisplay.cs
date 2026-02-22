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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateJobDisplayRow()
    {   
        int childCount = displayRow.transform.childCount;
    for (int i = childCount - 1; i >= 0; i--)
    {
        GameObject.Destroy(transform.GetChild(i).gameObject);
    }
       List<Job> jobQueue = GameManager.Instance._jobManager.jobQueue;
       jobQueue.Reverse();
         jobQueue.ForEach(job => AddJobToDisplayRow(job));
    }

    private void AddJobToDisplayRow(Job newJob)
    {
        JobDisplayCard newJobDisplayCard = Instantiate(jobDisplayCardPrefab,transform);
        newJobDisplayCard.job = newJob;
    }
}
