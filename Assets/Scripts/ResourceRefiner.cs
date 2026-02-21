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
    void Update()
    {
        if (Input.GetButtonDown("Interact") && NearRefiner)
        {
            if (GameManager.Instance._player.CarryObject == ExpectedResource)
            {
                InputResource = GameManager.Instance._player.CarryObject;
                GameManager.Instance._player.CarryObject = null;
                GameManager.Instance._player.CarryingObject.SetActive(false);

                StartCoroutine(Process());
            }
        }
        if(Input.GetButtonDown("Interact") && FinishedProcessing)
        {
            GameManager.Instance._player.CarryObject = OutputResource;
            GameManager.Instance._player.CarryingObject.SetActive(true);
            FinishedProcessing = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        NearRefiner = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        NearRefiner = false;
    }
    IEnumerator Process()
    {
        Processing = true;
        ProcessingIcon.SetActive(true);
        yield return new WaitForSeconds(ProcessTime);
        Processing = false;
        ProcessingIcon.SetActive(false);
        FinishedProcessing = true;
    }
}
