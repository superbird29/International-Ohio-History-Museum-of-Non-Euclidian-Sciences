using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.Rendering;

public class TheEgg : MonoBehaviour
{
    //Egg Bar Vars
    [SerializeField] int EggDifficulty = 0;
    [SerializeField] float ProgressSpeed = 1;
    [SerializeField] float ProgressAmount = .01f;
    [SerializeField] float ProgressTotal = 0;
    [SerializeField] GameObject Level;
    [SerializeField] Slider EggSlider;

    //Egg Challange Vars
    [SerializeField] List<GameObject> EggPannel; //list of egg pannels
    [SerializeField] List<char> RandomKeys;
    [SerializeField] bool KeyChosen = false;
    [SerializeField] GameObject EggPannelOneGreenCircle;
    [SerializeField] GameObject EggPannelTwoGreenCircle;
    [SerializeField] bool ReductionActive = false;
    [SerializeField] float ReductionAmount = .05f;
    [SerializeField] char ChosenKey;

    void Start()
    {
        //Start Progress Egg Timer
        StartCoroutine(ProgressEgg());
    }

    void Update()
    {
        EggPannel[EggDifficulty].SetActive(true);
        if(EggDifficulty == 0)
        {
            if (Input.GetButton("Interact"))
            {
                EggPannelOneGreenCircle.SetActive(true);
                ReductionActive = true;
            }
            else
            {
                EggPannelOneGreenCircle.SetActive(false);
                ReductionActive = false;
            }
        }
        if (EggDifficulty == 1) // || Input.GetButton("Interact2") || Input.GetButton("Interact3")
        {
            if(KeyChosen == false)
            {
                ChoseKey();
                KeyChosen = true;
            }
            EggPannel[EggDifficulty].GetComponentInChildren<TMP_Text>().text = "Tap " + ChosenKey;
            if (Input.GetButton("Interact"))
            {
                EggPannelTwoGreenCircle.GetComponentInChildren<TMP_Text>().text = "E " + ChosenKey;
                if(ChosenKey == 'Q' && Input.GetButtonDown("Interact1"))
                {
                    EggPannelTwoGreenCircle.SetActive(true);
                    ProgressTotal -= ReductionAmount;
                    ChoseKey();
                }
                else if (ChosenKey == 'R' && Input.GetButtonDown("Interact2"))
                {
                    EggPannelTwoGreenCircle.SetActive(true);
                    ProgressTotal -= ReductionAmount;
                    ChoseKey();
                }
                else if (ChosenKey == 'F' && Input.GetButtonDown("Interact3"))
                {
                    EggPannelTwoGreenCircle.SetActive(true);
                    ProgressTotal -= ReductionAmount;
                    ChoseKey();
                }
                else
                {
                    //EggPannelTwoGreenCircle.SetActive(false);
                }

            }
            else
            { 
                EggPannelTwoGreenCircle.SetActive(false);
            }
        }
        if (EggDifficulty == 2)
        {
            if (Input.GetButton("Interact"))
            {

            }
            else
            {

            }
        }
    }
    /// <summary>
    /// Area around Egg, Used to interact with egg.
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        ChoseKey();
        EggPannel[EggDifficulty].SetActive(true);
        if (Input.GetButton("Interact"))
        {
            EggPannelOneGreenCircle.SetActive(true);
            ReductionActive = true;
        }
        else
        {
            EggPannelOneGreenCircle.SetActive(false);
            ReductionActive = false;
        }
    }
    void ChoseKey()
    {
        ChosenKey = RandomKeys[Random.Range(0, 3)];
    }

    void IncreaseEggDifficulty()
    {
        EggDifficulty += 1;
        gameObject.GetComponent<TMP_Text>().text = "Level: " + EggDifficulty.ToString();
    }

    void EggProgress()
    {
        StartCoroutine(ProgressEgg());
        //add bar shaking and if its high increase it and add red
    }

    IEnumerator ProgressEgg()
    {
        yield return new WaitForSeconds(ProgressSpeed);
        if(ReductionActive)
        {
            ProgressTotal -= ReductionAmount;
        }
        else
        {
            ProgressTotal += ProgressAmount;
        }
        EggSlider.value = ProgressTotal;
        EggProgress();
    }
}
