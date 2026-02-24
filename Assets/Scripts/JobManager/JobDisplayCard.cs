using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JobDisplayCard : MonoBehaviour
{
    [SerializeField] TMP_Text jobTypeDisplay;
    [SerializeField] TMP_Text timerCountdown;

    [SerializeField] Image timerImage;

    [SerializeField] Color GreenTimer;
    [SerializeField] Color YellowTimer;

    [SerializeField] Color RedTimer;

    public Job job;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerCountdown.text = ((int)job.GetTimeRemaining()).ToString();
        jobTypeDisplay.text = job.jobType.ToString();
        if(job.GetTimeRemaining() > 30)
        {
            timerImage.color = GreenTimer;
        } else if(job.GetTimeRemaining() > 10)
        {
            timerImage.color = YellowTimer;
        } else
        {
            timerImage.color = RedTimer;
        }
    }
}
