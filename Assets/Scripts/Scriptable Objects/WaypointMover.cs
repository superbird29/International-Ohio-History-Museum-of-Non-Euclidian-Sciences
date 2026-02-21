using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{

    public Transform waypointParent;
    public float moveSpeed = 2f;
    public float waitTime = 0.1f;
    public bool loopWaypoints = true;

    private Transform[] waypoints;
    private int currentWaypointIndex;
    private bool isWaiting = false;
    private NPCManager NPC;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        NPC = GetComponent<NPCManager>();
        waypointParent = NPC.chosenPath;
        waypoints = new Transform[NPC.chosenPath.childCount];

        for (int i = 0; i < waypointParent.childCount; i++)
        {
            waypoints[i] = waypointParent.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        NPCManager NPC = GetComponent<NPCManager>();
        if (isWaiting || NPC.NPCState != "move" )
        {
            animator.SetBool("isWalking", false);
            return;
        }

        MoveToWaypoint();

    }

    void MoveToWaypoint()
    {
        Transform target = waypoints[currentWaypointIndex];
        Vector2 direction = (target.position - transform.position).normalized;

        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        animator.SetFloat("InputX", direction.x);
        animator.SetFloat("InputY", direction.y);
        animator.SetBool("isWalking", direction.magnitude > 0f);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
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

        if (currentWaypointIndex + 1 >= waypoints.Length)
        {
            Debug.Log("QuestTime");
            NPC.NPCState = "quest";
        }

        isWaiting = false;
    }
}
