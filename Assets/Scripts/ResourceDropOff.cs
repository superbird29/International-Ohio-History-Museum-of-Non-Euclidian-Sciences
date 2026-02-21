using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDropOff : MonoBehaviour
{
    [SerializeField] ResourceBox ExpectedResource;
    [SerializeField] bool NearDropOff = false;

    void Update()
    {
        if (Input.GetButtonDown("Interact") && NearDropOff)
        {
            if (GameManager.Instance._player.CarryObject == ExpectedResource)
            {
                GameManager.Instance._player.CarryObject = null;
                GameManager.Instance._player.CarryingObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        NearDropOff = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        NearDropOff = false;
    }
}
