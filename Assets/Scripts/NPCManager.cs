using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



public class NPCManager : MonoBehaviour
{
    private List<string> questStrings = new List<string> { "gift", "tour", "card" };
    public List<DialogueScriptableObject> DialogueListTours; 
    public List<DialogueScriptableObject> DialogueListGifts; 
    public List<DialogueScriptableObject> DialogueListCards;
    public List<CinemachinePathBase> WalkingPaths;

    //NPC states are spawn, move, quest, 
    public string NPCState = "spawn";

    public bool gaveQuest = false;

    //Variables to follow player for quest
    public Transform playerTarget;

    [SerializeField]private float followSpeed = 1f;

    private string chosenQuest;
    private DialogueScriptableObject chosenDialogue;
    private CinemachinePathBase chosenPath;
    private float maxPosition;

    // Start is called before the first frame update
    void Start()
    {
        //Setting up the NPC quest, their walking path, and dialogue.
        DialogueActivator dialogueActivator = GetComponent<DialogueActivator>();
        CinemachineDollyCart walkingPath = GetComponent<CinemachineDollyCart>();
        chosenQuest = GetRandomQuest();
        chosenDialogue = GetRandomDialogue(chosenQuest);
        chosenPath = GetWalkingPath(chosenQuest);
        chosenQuest = "tour";
        dialogueActivator.dialogueObject = chosenDialogue;
        walkingPath.m_Path = chosenPath;
        maxPosition = walkingPath.m_Path.MaxPos;
        NPCState = "move";

        //Store follow values for the tour quest
     
    }

    public string GetRandomQuest()
    {
        if (questStrings.Count == 0)
        {
            Debug.Log("Empty list error");
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, questStrings.Count);
        return questStrings[randomIndex];
    }

    public DialogueScriptableObject GetRandomDialogue(string chosenQuest)
    {
        int randomIndex;
        switch (chosenQuest)
        {
            case "gift":

                if (DialogueListGifts.Count == 0)
                {
                    Debug.Log("Empty list error");
                    return null;
                }

                randomIndex = UnityEngine.Random.Range(0, DialogueListGifts.Count);
                return DialogueListGifts[randomIndex];

                break;
            case "tour":

                if (DialogueListTours.Count == 0)
                {
                    Debug.Log("Empty list error");
                    return null;
                }

                randomIndex = UnityEngine.Random.Range(0, DialogueListTours.Count);
                return DialogueListTours[randomIndex];

                break;
            case "card":

                if (DialogueListCards.Count == 0)
                {
                    Debug.Log("Empty list error");
                    return null;
                }

                randomIndex = UnityEngine.Random.Range(0, DialogueListCards.Count);
                return DialogueListCards[randomIndex];

                break;
            case "default":
                return null;
                break;
        }

        return null;
    }


    public CinemachinePathBase GetWalkingPath(string chosenQuest)
    {
        switch (chosenQuest)
        {
            case "card":
                return WalkingPaths[0];
                break;

            case "gift":

                return WalkingPaths[1];
                break;

            case "tour":

                return WalkingPaths[2];
                break;


            case "default":
                return null;
        }

        return null;
    }


    // Update is called once per frame
    private void Update()
    {
        if (NPCState == "move")
        {
            CinemachineDollyCart walkingPath = GetComponent<CinemachineDollyCart>();
            if (walkingPath.m_Position >= maxPosition)
            {
                NPCState = "quest";
            }
        }
    }

    void LateUpdate()
    {
        if (NPCState == "quest" && gaveQuest == true)
        {
            if (chosenQuest == "tour")
            {

                transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, followSpeed * Time.deltaTime);
            }
        }
    }
}
 