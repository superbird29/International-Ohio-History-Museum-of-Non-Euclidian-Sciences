using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDropOff : MonoBehaviour
{
    [SerializeField] ResourceBox ExpectedResource;
    [SerializeField] bool NearDropOff = false;

    [SerializeField] GiftShopJobManager giftShopJobManager;

    void Update()
    {
        if (Input.GetButtonDown("Interact") && NearDropOff)
        {
            if (GameManager.Instance._player.CarryObject == ExpectedResource)
            {
                GameManager.Instance._player.CarryObject = null;
                GameManager.Instance._player.CarryingObject.SetActive(false);
                if(giftShopJobManager.jobQueue.Count > 0){
                giftShopJobManager.CompleteNextJob();
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
        GameManager.Instance._player.ActiveInteractPopup();
        NearDropOff = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
        GameManager.Instance._player.DeactiveInteractPopup();
        NearDropOff = false;
        }
    }
}
