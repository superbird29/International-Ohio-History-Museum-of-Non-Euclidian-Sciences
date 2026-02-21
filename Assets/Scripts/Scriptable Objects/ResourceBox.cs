using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObjects/New Resource", order = 0)]
public class ResourceBox : ScriptableObject
{
    public GameObject Resource;
    public GameObject Icon;
}
