using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverlayController : MonoBehaviour
{
    public List<Sprite> MouseOverLaySprite;
    public GameObject MouseOverLayObj;


    public void ChangeMouseSprite(int sprite)
    {
        MouseOverLayObj.GetComponent<SpriteRenderer>().sprite = MouseOverLaySprite[sprite];
    }

    public void ClearMouseSprite()
    {
        MouseOverLayObj.GetComponent<SpriteRenderer>().sprite = MouseOverLaySprite[0];
    }
}
