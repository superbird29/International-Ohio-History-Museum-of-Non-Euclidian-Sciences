using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GiftOrder : ScriptableObject
{
    public abstract void DoAction();
    public GameObject Prefab;
}
