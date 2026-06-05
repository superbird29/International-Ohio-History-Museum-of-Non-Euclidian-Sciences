using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelpText : MonoBehaviour
{
    [SerializeField] GameObject TextObjEggMiniGame;
    public List<string> TextOptionsEggMiniGame;
    [SerializeField] int CurrentlySelectedTextEggMiniGame;


    private void Update()
    {
        TextObjEggMiniGame.GetComponent<TMP_Text>().text = TextOptionsEggMiniGame[CurrentlySelectedTextEggMiniGame];
    }

    public void ChangeTextEggMiniGame(int num)
    {
        CurrentlySelectedTextEggMiniGame = num;
    }
}
