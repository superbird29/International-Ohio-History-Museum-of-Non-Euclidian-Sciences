using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public float textSpeed;
    public string[] lines;
    private int index;


    private void StartDialogue(float newTextSpeed, string[] newLines)
    {
        lines = newLines;
        textSpeed = newTextSpeed;
        textComponent.text = string.Empty;
        RunDialogue();
    }

    void Update()
    {

        //This allows the dialogue to skip, should be switched to interact button or removed
        if (Input.GetMouseButtonDown(0))
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }

    void RunDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
