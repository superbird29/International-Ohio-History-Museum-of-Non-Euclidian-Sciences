using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EggManager2 : MonoBehaviour
{
    [SerializeField] List<GameObject> Speakers;


    //candle Mini Game Vars
    [SerializeField] List<GameObject> CandlesLight;
    [SerializeField] bool LighterLit = false;
    [SerializeField] GameObject LighterLitIcon;
    [SerializeField] bool MatchLight = false;
    [SerializeField] GameObject OverMouseIcon;
    [SerializeField] GameObject MatchIcon;
    [SerializeField] GameObject OrangeGlow;
    [SerializeField] Sprite UnlitMatchMouseIcon;
    [SerializeField] Sprite LitMatchMouseIcon;
    public bool MouseOverLAyOn = true;
    [SerializeField] int LighterfailedLight = 0;
    [SerializeField] List<string> InstructionFieldTxt;
    [SerializeField] GameObject InstructionField;
    public bool ElectroTapeActive = false;
    public GameObject OverMouseTapeIcon;


    private void Update()
    {
        //lighter in hand
        if (MouseOverLAyOn)
        {
            OverMouseIcon.SetActive(true);
            OverMouseIcon.transform.position = Input.mousePosition;
            Cursor.visible = false;
        }
        //electrical tape in hand
        else if (ElectroTapeActive)
        {
            OverMouseTapeIcon.SetActive(true);
            OverMouseTapeIcon.transform.position = Input.mousePosition;
            Cursor.visible = false;
        }
        //nothing in hand
        else
        {
            OverMouseTapeIcon.SetActive(false);
            OverMouseIcon.SetActive(false);
            Cursor.visible = true;
        }
    }


    /// <summary>
    /// Called when the player clicks on the lighter.
    /// Changes MatchLight to true and activates the match the 
    /// </summary>
    public void LighterInteraction()
    {
        if (!LighterLit)
        {
            LighterfailedLight += 1;
            if (Random.Range(1, 3) == 1 || LighterfailedLight >= 3)
            {
                InstructionField.GetComponent<TMP_Text>().text = InstructionFieldTxt[1];
                LighterLit = true;
                LighterLitIcon.SetActive(true);

                MatchLight = true;
                MatchIcon.GetComponent<Image>().sprite = LitMatchMouseIcon;
                OrangeGlow.SetActive(true);
                InstructionField.GetComponent<TMP_Text>().text = InstructionFieldTxt[2];
            }
        }
    }
    /// <summary>
    /// lights the candle 
    /// </summary>
    /// <param name="Candle"></param>
    public void LightCandle(int candle)
    {
        if (MatchLight && CandlesLight[candle].activeSelf == false)
        {
            CandlesLight[candle].SetActive(true);
            Matchout();
        }
        if(CheckforAllCandlesLit())
        {
            InstructionField.GetComponent<TMP_Text>().text = InstructionFieldTxt[3];
        }
    }

    void Matchout()
    {
        if (Random.Range(1, 4) == 1)
        {
            InstructionField.GetComponent<TMP_Text>().text = InstructionFieldTxt[1];
            MatchLight = false;
            MatchIcon.GetComponent<Image>().sprite = UnlitMatchMouseIcon;
            OrangeGlow.SetActive(false);
        }
    }
    
    public void ElectricalTapButton()
    {
        ElectroTapeActive = true;
    }

    bool CheckforAllCandlesLit()
    {
        int AllCandlesLit = 0;

        for (int i = 0; i < CandlesLight.Count; i++)
        {
            if (CandlesLight[i])
            {
                AllCandlesLit++;
            }
        }

        if (AllCandlesLit == CandlesLight.Count - 1)
        {
            return true;
        }

        return false;
    }
}
