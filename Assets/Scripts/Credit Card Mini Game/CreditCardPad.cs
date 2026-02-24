using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreditCardPad : MonoBehaviour
{
    [SerializeField] TMP_Text numberDisplay;

    [SerializeField] TMP_Text creditCardNumber;

    [SerializeField] float resultDisplayTime;

    [SerializeField] TicketCounter ticketCounter;
    CreditCard creditCard;

    bool checkingNumber;

    bool cardStarted;

    string enteredNumber;

     void Awake()
    {
        creditCard = ScriptableObject.CreateInstance<CreditCard>();
        enteredNumber = "";
        checkingNumber = false;
        cardStarted = false;
    }

     void Update()
    {
        if (enteredNumber.Length >= 9 && !checkingNumber)
        {
            checkingNumber = true;
            ValidateNumber();
        }
    }

    public void BeginMinigame()
    {
        if (!cardStarted)
        {
            GenerateCreditCard();
            ClearAll();
            checkingNumber = false;
            cardStarted = true;
        }
    }

    public void ForceStartMinigame()
    {
        
            GenerateCreditCard();
            ClearAll();
            checkingNumber = false;
            cardStarted = true;
    }

    public void EnterNumber(string number)
    {
        AddToNumberDisplay(number);
    }

    public void ClearAll()
    {
        ClearDisplay();
        ClearNumber();
    }

    public void Backspace()
    {
        enteredNumber = enteredNumber.Remove(enteredNumber.Length - 1, 1);
        numberDisplay.text = enteredNumber;
    }

    public void GenerateCreditCard()
    {
        creditCard.GenerateNumber();
        creditCardNumber.text = creditCard.displayNumber;
    }

    public void AddToNumberDisplay(string number)
    {
        if (!checkingNumber)
        {
            enteredNumber += number;
            numberDisplay.text = enteredNumber;
        }
    }

    public void ClearDisplay()
    {
        numberDisplay.text = "";
    }
    
    public void ClearNumber()
    {
        enteredNumber = "";
    }

    public void PostValidateCleanup()
    {
        ClearAll();
        checkingNumber = false;
    }

    public void ValidateNumber()
    {
        if (creditCard.ValidateEnteredNumber(enteredNumber))
        {
            StartCoroutine(Accepted());
        }
        else
        {
            StartCoroutine(Declined());
        }
    }

    IEnumerator Accepted()
    {
        ticketCounter.CCEntered();
        cardStarted = false;
        numberDisplay.text = "ACCEPTED";

        yield return new WaitForSeconds(resultDisplayTime);

        numberDisplay.text = "";

        yield return new WaitForSeconds(resultDisplayTime);

        numberDisplay.text = "ACCEPTED";

        yield return new WaitForSeconds(resultDisplayTime);

        PostValidateCleanup();
        ticketCounter.DisableCCPadCanvas();
        
    }

    IEnumerator Declined()
    {
        numberDisplay.text = "DECLINED";

        yield return new WaitForSeconds(resultDisplayTime);

        numberDisplay.text = "";

        yield return new WaitForSeconds(resultDisplayTime);

        numberDisplay.text = "DECLINED";

        yield return new WaitForSeconds(resultDisplayTime);

        PostValidateCleanup();
    }
}
