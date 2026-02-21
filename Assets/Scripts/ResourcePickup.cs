using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickup : MonoBehaviour
{
    [SerializeField] ResourceBox Resource;
    [SerializeField] bool NearBox = false;

    void Update()
    {
        if (Input.GetButtonDown("Interact") && NearBox)
        {
            GameManager.Instance._player.CarryObject = Resource;
            GameManager.Instance._player.CarryingObject.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        NearBox = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        NearBox = false;
    }
}
