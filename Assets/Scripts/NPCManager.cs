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


    //Movement Script
    public Transform waypointParent;
    public float moveSpeed = 2f;
    public float waitTime = 0.1f;
    public bool loopWaypoints = false;

    private Transform[] waypoints;
    private int currentWaypointIndex;
    private bool isWaiting = false;
    private Animator animator;


    // Start is called before the first frame update
    void Awake()
    {
        //Setting up the NPC quest, their walking path, and dialogue.
        DialogueActivator dialogueActivator = GetComponent<DialogueActivator>();

        //chosenQuest = GetRandomQuest();
        chosenQuest = "tour";
        chosenDialogue = GetRandomDialogue(chosenQuest);
        waypointParent = GetWalkingPath(chosenQuest);
        dialogueActivator.dialogueObject = chosenDialogue;

        if (chosenQuest == "tour"){
            chosenTour = GetRandomTour();
        }


        animator = GetComponent<Animator>();

        waypoints = new Transform[waypointParent.childCount];
        for (int i = 0; i < waypointParent.childCount; i++)
        {
            waypoints[i] = waypointParent.GetChild(i);
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

        return null;
    }


    // Update is called once per frame
    private void Update()
    {
        if (isWaiting || NPCState != "move")
        {
            if (NPCState == "quest" && chosenQuest == "tour")
            {

            }
            else
            {
                animator.SetBool("isWalking", false);
                return;
            }
        }



        if (NPCState == "move")
        {
            animator.SetBool("isWalking", true);
            MoveToWaypoint();

        }

        if (NPCState == "quest" && gaveQuest == true)
        {
            if (chosenQuest == "tour")
            {
                Vector2 direction = (playerTarget.position - transform.position).normalized;
                transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, followSpeed * Time.deltaTime);
                chosenTour.SetActive(true);

                animator.SetFloat("InputX", direction.x);
                animator.SetFloat("InputY", direction.y);
                animator.SetBool("isWalking", direction.magnitude > 0f);

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
            animator.SetBool("isWalking", false);

        }
    }


    void MoveToWaypoint()
    {
        Transform target = waypoints[currentWaypointIndex];
        Vector2 direction = (target.position - transform.position).normalized;


        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);



        animator.SetFloat("InputX", direction.x);
        animator.SetFloat("InputY", direction.y);
        animator.SetBool("isWalking", direction.magnitude > 0f);


        if (Vector2.Distance(transform.position, target.position) < 0.2f)
        {

            StartCoroutine(WaitAtWaypoint());

        }

    }


    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        animator.SetBool("isWalking", false);
        yield return new WaitForSeconds(waitTime);

        //The % allows the waypoints to wrap around in a circle
        //If looping, increment currentwaypointindex and warp around, If NOT looping, increment waypoint but done exceed the final waypoint. 
        currentWaypointIndex = loopWaypoints ? (currentWaypointIndex + 1) % waypoints.Length : Mathf.Min(currentWaypointIndex + 1, waypoints.Length - 1);

        if (currentWaypointIndex + 1>= waypoints.Length)
        {
            Debug.Log("QuestTime");
            NPCState = "quest";
            animator.SetBool("isWalking", false);
        }

        isWaiting = false;
    }

    private void QuestComplete()
    {
        //What happens when the quest is done! 




        Destroy(gameObject);
    }
}
 