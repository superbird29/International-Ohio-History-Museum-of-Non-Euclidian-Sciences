using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TouristWaitingArea : MonoBehaviour
{

    [SerializeField] List<TouristSpawnPoint> touristSpawnPoints = new();

    void Start()
    {
        touristSpawnPoints.AddRange(GetComponentsInChildren<TouristSpawnPoint>());
        foreach(TouristSpawnPoint touristSpawnPoint in touristSpawnPoints){
            touristSpawnPoint.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void AssignTouristToSpawnPoint(Tourist tourist)
    {
        TouristSpawnPoint availableSpawnPoint = touristSpawnPoints.Where(touristSpawnPoint => !touristSpawnPoint.hasTourist).ToList()[0];
        availableSpawnPoint.hasTourist = true;
        tourist.touristSpawnPoint = availableSpawnPoint;
    }
}
