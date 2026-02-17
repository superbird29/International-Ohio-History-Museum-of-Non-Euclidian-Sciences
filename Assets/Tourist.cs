using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Tourist : MonoBehaviour
{
    bool playerInRange;

    bool followingPlayer;

    [SerializeField] Canvas displayPrompt;

    [SerializeField] float touristSpeed;

    [SerializeField] float stoppingDistance;

    Transform playerTransform;

    void Start()
    {
        playerInRange = false;
        displayPrompt.gameObject.SetActive(false);
        Debug.Log(GetComponent<CircleCollider2D>().forceReceiveLayers.ToString());
    }

    void Update()
    {
        

        if (followingPlayer)
        {
            Vector3 direction = playerTransform.position - transform.position;
            float distance = direction.magnitude;
            if (distance > stoppingDistance)
            {
                Vector3 targetPosition = playerTransform.position - direction.normalized * stoppingDistance;

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, touristSpeed * Time.deltaTime);
            }
        }

        if (followingPlayer && Input.GetButtonDown("Interact"))
        {
            followingPlayer = false;
        }

        if (playerInRange && Input.GetButtonDown("Interact"))
        {
            followingPlayer = true;        
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            displayPrompt.gameObject.SetActive(true);
            playerTransform = collision.gameObject.transform;            
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            displayPrompt.gameObject.SetActive(false);
        }
    }
}
