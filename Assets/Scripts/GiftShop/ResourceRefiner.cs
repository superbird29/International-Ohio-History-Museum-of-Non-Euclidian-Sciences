using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceRefiner : MonoBehaviour
{
    [SerializeField] ResourceBox InputResource;
    [SerializeField] ResourceBox ExpectedResource;
    [SerializeField] ResourceBox OutputResource;
    [SerializeField] float ProcessTime = 10f;
    [SerializeField] bool Processing = false;
    [SerializeField] bool FinishedProcessing = false;
    [SerializeField] bool NearRefiner = false;
    [SerializeField] GameObject ProcessingIcon;
    [SerializeField] GameObject FinishedIcon;
    public GameObject NextIcon;
    [SerializeField] GameObject NextObj;
    void Update()
    {
        if (Input.GetButtonDown("Interact") && NearRefiner)
        {
            if (GameManager.Instance._player.CarryObject == ExpectedResource)
            {
                InputResource = GameManager.Instance._player.CarryObject;
                GameManager.Instance._player.CarryObject = null;
                GameManager.Instance._player.CarryingObject.SetActive(false);
                NextIcon.SetActive(false);
                StartCoroutine(Process());
            }
        }
        if(Input.GetButtonDown("Interact") && FinishedProcessing && NearRefiner)
        {
            GameManager.Instance._player.CarryObject = OutputResource;
            GameManager.Instance._player.CarryingObject.SetActive(true);
            FinishedProcessing = false;
            FinishedIcon.SetActive(false);
            NextObj.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
        GameManager.Instance._player.ActiveInteractPopup();
        NearRefiner = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
        GameManager.Instance._player.DeactiveInteractPopup();
        NearRefiner = false;
        }
    }
    IEnumerator Process()
    {
        Processing = true;
        ProcessingIcon.SetActive(true);
        ProgressColor();
        yield return new WaitForSeconds(ProcessTime);
        Processing = false;
        ProcessingIcon.SetActive(false);
        FinishedProcessing = true;
        FinishedIcon.SetActive(true);
        ProcessingIcon.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

    void ProgressColor()
    {
        if(Processing == true)
        {
            StartCoroutine(ProgressBar());
        }
    }

    IEnumerator ProgressBar()
    {
        yield return new WaitForSeconds(1);
        ProcessingIcon.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(1);
        ProcessingIcon.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        print("one pass");
        ProgressColor();
    }
}
