using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class SpeakerSlider : MonoBehaviour
{
    public GameObject GreenObj;
    public Button buttonObj;
    public GameObject Slider;
    public float Speed = .4f;
    public bool ChangeDirection = false;
    public float AdjustedTargetArea;
    public float GraceAmount = .05f;
    public float BonusGraceAmount = .01f;
    public int BonusGraceMissTime = 0;
    public GameObject SparkingObj;

    public void Activate()
    {
        buttonObj.enabled = true;
        Slider.SetActive(true);
        //move Green Dot
        float temp = Random.Range(-70, 70);
        GreenObj.transform.localPosition = new Vector3(0, temp, 0);
        AdjustedTargetArea = (temp + 70) / 140;
    }

    void Update()
    {
        //move slider back and forth
        if (!ChangeDirection)
        {
            Slider.GetComponent<Slider>().value += Speed * Time.deltaTime;
        }
        else
        {
            Slider.GetComponent<Slider>().value -= Speed * Time.deltaTime;
        }

        //switches direction when at certian values 
        if(Slider.GetComponent<Slider>().value == 1)
        {
            ChangeDirection = true;
        }
        if (Slider.GetComponent<Slider>().value == 0)
        {
            ChangeDirection = false;
        }
    }

    public void CheckClick()
    {
        //print(AdjustedTargetArea + .05f + "Higher Bound");
        //print(AdjustedTargetArea - .05f + "Lower Bound");
        //print(Slider.GetComponent<Slider>().value + "Goal");
        if (Slider.GetComponent<Slider>().value <= AdjustedTargetArea + (GraceAmount + (BonusGraceAmount * BonusGraceMissTime)) && 
            Slider.GetComponent<Slider>().value >= AdjustedTargetArea - (GraceAmount + (BonusGraceAmount * BonusGraceMissTime)))
        {
            print("hit");
            Slider.SetActive(false);
        }
        else
        {
            BonusGraceMissTime += 1;
            print("miss");
            CheckMalfunction();
            return;
        }
    }

    void CheckMalfunction()
    {
        if(Random.Range(0,4) == 1)
        {
            Slider.SetActive(false);
            buttonObj.enabled = false;
            SparkingObj.SetActive(true);
        }
    }

    public void FixShort()
    {
        print("mep");
        if(GameManager.Instance._eggManager.ElectroTapeActive)
        {
            SparkingObj.SetActive(false);
            Activate();
            GameManager.Instance._eggManager.ElectroTapeActive = false;
        }
    }
}
