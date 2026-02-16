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
    CreditCard creditCard;

    bool checkingNumber;

    string enteredNumber;

    public void Start()
    {
        creditCard = ScriptableObject.CreateInstance<CreditCard>();
        BeginMinigame();
    }

    public void Update()
    {
        if (enteredNumber.Length >= 12 && !checkingNumber)
        {
            checkingNumber = true;
            ValidateNumber();
            StartCoroutine(DisplayResult());
            ClearNumber();
        }
    }

    public void BeginMinigame()
    {
        GenerateCreditCard();
        ClearDisplay();
        checkingNumber = false;
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

    public void ValidateNumber()
    {
        if (creditCard.ValidateEnteredNumber(enteredNumber))
        {
            numberDisplay.text = "ACCEPTED";
            GenerateCreditCard();
        }
        else
        {
            numberDisplay.text = "DECLINED";
        }

    }

    IEnumerator DisplayResult()
    {
        yield return new WaitForSeconds(resultDisplayTime);

        string text = numberDisplay.text;

        numberDisplay.text = "";

        yield return new WaitForSeconds(resultDisplayTime);

        numberDisplay.text = text;

        yield return new WaitForSeconds(resultDisplayTime);

        numberDisplay.text = "";
        checkingNumber = false;
    }
}
