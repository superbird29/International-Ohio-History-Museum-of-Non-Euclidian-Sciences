using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class EggManager : MonoBehaviour
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
    [SerializeField] bool Inside = false;

    void Start()
    {
        //Start Progress Egg Timer
        StartCoroutine(ProgressEgg());
        StartCoroutine(EggPatience());
    }

    void Update()
    {
        if (EggDifficulty == 0 && Inside)
        {
            if (Input.GetButton("Interact"))
            {
                EggPannelOneGreenCircle.SetActive(true);
                ReductionActive = true;
                print("mep");
            }
            else
            {
                EggPannelOneGreenCircle.SetActive(false);
                ReductionActive = false;
            }
        }
        if (EggDifficulty == 1 && Inside)
        {
            EggPannel[EggDifficulty].GetComponentInChildren<TMP_Text>().text = "Tap " + ChosenKey;
            if (Input.GetButton("Interact"))
            {
                EggPannelTwoGreenCircle.GetComponentInChildren<TMP_Text>().text = "E " + ChosenKey;
                if (ChosenKey == 'Q' && Input.GetButtonDown("Interact1"))
                {
                    print("q");
                    EggPannelTwoGreenCircle.SetActive(true);
                    ProgressTotal -= ReductionAmount;
                    ChoseKey();
                }
                else if (ChosenKey == 'R' && Input.GetButtonDown("Interact2"))
                {
                    print("r");
                    EggPannelTwoGreenCircle.SetActive(true);
                    ProgressTotal -= ReductionAmount;
                    ChoseKey();
                }
                else if (ChosenKey == 'F' && Input.GetButtonDown("Interact3"))
                {
                    print("f");
                    EggPannelTwoGreenCircle.SetActive(true);
                    ProgressTotal -= ReductionAmount;
                    ChoseKey();
                }
                else
                {
                    //EggPannelTwoGreenCircle.SetActive(false);
                    print("guys");
                }

            }
            else
            {
                EggPannelTwoGreenCircle.SetActive(false);
            }
        }
        if (EggDifficulty == 2 && Inside)
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
    public void OnTriggerEnter2D(Collider2D collision)
    {
        ChoseKey();
        Inside = true;
        EggPannel[EggDifficulty].SetActive(true);
        
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        Inside = false;
        for (int i = 0; i < EggPannel.Count; i++)
        {
            EggPannel[i].SetActive(false);
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
        StartCoroutine(EggPatience());
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
    IEnumerator EggPatience()
    {
        yield return new WaitForSeconds(ProgressSpeed);
        if (ReductionActive)
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
