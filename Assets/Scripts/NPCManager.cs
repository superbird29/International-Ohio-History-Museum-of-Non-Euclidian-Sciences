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
    public List<GameObject> TourLocations;
    public List<CinemachinePathBase> WalkingPaths;

    private Animator animator;
    private Rigidbody2D rb;
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
    private CinemachinePathBase chosenPath;
    private GameObject chosenTour;
    private float maxPosition;

    // Start is called before the first frame update
    void Start()
    {
        //Setting up the NPC quest, their walking path, and dialogue.
        DialogueActivator dialogueActivator = GetComponent<DialogueActivator>();
        CinemachineDollyCart walkingPath = GetComponent<CinemachineDollyCart>();
        chosenQuest = GetRandomQuest();
        //chosenQuest = "tour";
        chosenDialogue = GetRandomDialogue(chosenQuest);
        chosenPath = GetWalkingPath(chosenQuest);
        dialogueActivator.dialogueObject = chosenDialogue;
        walkingPath.m_Path = chosenPath;
        maxPosition = walkingPath.m_Path.MaxPos;
        NPCState = "move";

        if (chosenQuest == "tour"){
            chosenTour = GetRandomTour();
        }

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

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

        Quaternion.Euler(0, 0, 0);
        if (NPCState == "move")
        {
            
            Vector2 velocity = rb.velocity;

            if (velocity.magnitude > 0.1f) // Only change if moving
            {
                // 2. Prioritize Vertical movement for 4-way, then Horizontal
                if (Mathf.Abs(velocity.y) > Mathf.Abs(velocity.x))
                {
                    moveX = 0;
                    moveY = velocity.y > 0 ? 1 : -1; // Up : Down
                }
                else
                {
                    moveX = velocity.x > 0 ? 1 : -1; // Right : Left
                    moveY = 0;
                }
            }

            // 3. Optional: Send to Animator
            if (animator != null)
            {
                animator.SetFloat("InputX", moveX);
                animator.SetFloat("InputY", moveY);
                animator.SetBool("isWalking", true);
            }

            CinemachineDollyCart walkingPath = GetComponent<CinemachineDollyCart>();

            if (walkingPath.m_Position >= maxPosition)
            {
                NPCState = "quest";
                walkingPath.enabled = false;
            }
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

                animator.SetBool("isWalking", false);
                animator.SetFloat("LastInputX", animator.GetFloat("InputX"));
                animator.SetFloat("LastInputY", animator.GetFloat("InputY"));

            }
            if (NPCState != "quest")
            {
                animator.SetBool("isWalking", false);
                animator.SetFloat("LastInputX", animator.GetFloat("InputX"));
                animator.SetFloat("LastInputY", animator.GetFloat("InputY"));
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
            CinemachineDollyCart walkingPath = GetComponent<CinemachineDollyCart>();

            NPCState = "quest";
            walkingPath.enabled = false;
        }
    }

    private void QuestComplete()
    {
        //What happens when the quest is done! 




        Destroy(gameObject);
    }
}
 