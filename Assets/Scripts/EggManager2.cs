using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] bool MouseOverLAyOn = true;
    [SerializeField] int LighterfailedLight = 0;

    private void Update()
    {
        if(MouseOverLAyOn)
        {
            OverMouseIcon.transform.position = Input.mousePosition;
            Cursor.visible = false;
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
            if (Random.Range(1, 2) == 1 || LighterfailedLight >= 3)
            {
                LighterLit = true;
            }
        }
        else
        {
            MatchLight = true;
            MatchIcon.GetComponent<Image>().sprite = LitMatchMouseIcon;
            OrangeGlow.SetActive(true);
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
    }

    void Matchout()
    {
        if(Random.Range(1, 4) == 1)
        {
            MatchLight = false;
            MatchIcon.GetComponent<Image>().sprite = UnlitMatchMouseIcon;
            OrangeGlow.SetActive(false);
        }
    }
}
