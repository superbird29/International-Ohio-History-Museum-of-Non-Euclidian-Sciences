using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Tourist : MonoBehaviour
{
    bool playerInRange;

    public bool followingPlayer {get; set;}

    [SerializeField] float touristSpeed;

    [SerializeField] float stoppingDistance;

    Transform playerTransform;

    public TourDeliveryZone tourDeliveryZone {get; set;}

    public TouristSpawnPoint touristSpawnPoint {get; set;}

    void Start()
    {
        playerInRange = false;
        followingPlayer = false;
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
            if(!tourDeliveryZone.gameObject.activeSelf){
                tourDeliveryZone.gameObject.SetActive(true);
            }
        }

        if (followingPlayer && Input.GetButtonDown("Interact"))
        {
            followingPlayer = false;
        }

        if (playerInRange && Input.GetButtonDown("Interact"))
        {
            followingPlayer = true;
            GameManager.Instance._player.FollowingTourist = this;
        }

        if (touristSpawnPoint != null && !followingPlayer)
        {
            transform.position = touristSpawnPoint.transform.position;
        }
    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            GameManager.Instance._player.ActiveInteractPopup();
            playerTransform = collision.gameObject.transform;            
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            GameManager.Instance._player.DeactiveInteractPopup();
        }
    }

    void OnDisable()
    {
        if (followingPlayer)
        {
            GameManager.Instance._player.FollowingTourist = null;
        }
        touristSpawnPoint.hasTourist = false;
        tourDeliveryZone.tourist = null;
    }
}
