using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Response
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogueScriptableObject dialogueObject;

    public string ResponseText => responseText;

    public DialogueScriptableObject DialogueObject => dialogueObject;
}
