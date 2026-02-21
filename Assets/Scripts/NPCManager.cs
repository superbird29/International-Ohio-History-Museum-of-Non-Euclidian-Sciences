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
    public List<Transform> MovementLists;
    public List<GameObject> TourLocations;

    public float moveX { get; private set; }
    public float moveY { get; private set; }

    //NPC states are spawn, move, quest, 
    public string NPCState = "spawn";

    public bool gaveQuest = false;

    //Variables to follow player for quest
    public Transform playerTarget;

    [SerializeField]private float followSpeed = 1f;

    private string chosenQuest;
    private DialogueScriptableObject chosenDialogue;
    private GameObject chosenTour;
    public Transform chosenPath;

    // Start is called before the first frame update
    void Awake()
    {
        //Setting up the NPC quest, their walking path, and dialogue.
        DialogueActivator dialogueActivator = GetComponent<DialogueActivator>();

        chosenQuest = GetRandomQuest();
        //chosenQuest = "tour";
        chosenDialogue = GetRandomDialogue(chosenQuest);
        chosenPath = GetWalkingPath(chosenQuest);
        dialogueActivator.dialogueObject = chosenDialogue;

        if (chosenQuest == "tour"){
            chosenTour = GetRandomTour();
        }

        NPCState = "move";

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

    public GameObject GetRandomTour()
    {
        if (TourLocations.Count == 0)
        {
            Debug.Log("Empty list error");
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, TourLocations.Count);
        return TourLocations[randomIndex];
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


    public Transform GetWalkingPath(string chosenQuest)
    {
        switch (chosenQuest)
        {
            case "tour":
                return MovementLists[0];
                break;

            case "gift":

                return MovementLists[1];
                break;

            case "card":

                return MovementLists[2];
                break;


            case "default":
                return null;
        }
        Debug.Log(MovementLists);

        return null;
    }


    // Update is called once per frame
    private void Update()
    {

        if (NPCState == "move")
        {
            

        }

        if (NPCState == "quest" && gaveQuest == true)
        {
            if (chosenQuest == "tour")
            {
                transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, followSpeed * Time.deltaTime);
                chosenTour.SetActive(true);

            }
        }

        if (NPCState != "move")
        {
            if (NPCState == "quest" && chosenQuest != "tour")
            {


            }
            if (NPCState != "quest")
            {

            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == chosenTour)
        {
            QuestComplete();
            chosenTour.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Enemy") && NPCState == "move")
        {
            //Stop moving if another NPC is waiting in line ahead of you. 

            NPCState = "quest";

        }
    }

    private void QuestComplete()
    {
        //What happens when the quest is done! 




        Destroy(gameObject);
    }
}
 