using UnityEngine;

public class CreditCard : ScriptableObject
{
    public string number;
    public string displayNumber;

    public void GenerateNumber()
    {
        number = "";
        displayNumber = "";
        for (int i = 0; i < 9; i++)
        {
            if (i % 3 == 0)
            {
                displayNumber += " ";
            }
            int nextDigit = Random.Range(0, 10);
            number += nextDigit;
            displayNumber += nextDigit;
        }
    }

    public bool ValidateEnteredNumber(string enteredNumber)
    {
        return number.Equals(enteredNumber);
    }
}
