using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCollison : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        //GameManager.Instance._eggManager.EggZoneEnter();
    }
    void OnTriggerExit2D(Collider2D other)
    {
        //GameManager.Instance._eggManager.EggZoneLeave();
    }
}
