using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RentManager : MonoBehaviour
{
    private float currentSavings = 0f;
    public float lifetimeEarnings = 0f;

    [SerializeField] private float rentPaymentCountdown = 300f;
    [SerializeField] private float startingRentPayment = 20f;

    [SerializeField] private float rentGrowthFactor = 1.1f;

    [SerializeField] private TMP_Text currentSavingsText;
    [SerializeField] private TMP_Text nextRentPaymentText;
    [SerializeField] private TMP_Text timeUntilNextRentPaymentText;
    private float timeUntilNextRentPayment;

    private float currentRent;

    private bool payingRent;
    // Start is called before the first frame update
    void Start()
    {
        timeUntilNextRentPayment = rentPaymentCountdown;
        currentRent = startingRentPayment;
        nextRentPaymentText.text = "$" + String.Format("{0:0.00}", currentRent);
    }

    // Update is called once per frame
    void Update()
    {
        currentSavingsText.text = "$" + String.Format("{0:0.00}", currentSavings);
        
        timeUntilNextRentPayment = timeUntilNextRentPayment - Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeUntilNextRentPayment);
        timeUntilNextRentPaymentText.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        nextRentPaymentText.color = currentSavings >= currentRent ? Color.green : Color.red;
        if(timeUntilNextRentPayment <= 0f && !payingRent)
        {
            payingRent = true;
            PayRent();
            timeUntilNextRentPayment = rentPaymentCountdown;
            payingRent = false;
        }
    }

    public void AddMoney(float amount)
    {
        currentSavings += amount;
        lifetimeEarnings += amount;
    }

    void PayRent()
    {
        if(currentSavings >= currentRent)
        {
            currentSavings -= currentRent;
            currentRent *= rentGrowthFactor;
        } else
        {
            GameManager.Instance._eggManager.IncreaseEggBar(1 - (currentSavings/currentRent));
            currentSavings = 0f;
        }
        nextRentPaymentText.text = "$" + String.Format("{0:0.00}", currentRent);
    }

    
}
