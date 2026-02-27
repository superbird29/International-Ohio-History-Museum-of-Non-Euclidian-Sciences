using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TourMiniGameManager : LocalJobManager
{
    [SerializeField] List<TourDeliveryZone> tourDeliveryZones = new();
    
    [SerializeField] Tourist touristPrefab;

    [SerializeField] TouristWaitingArea touristWaitingArea;

    public override void JobAdded(Job addedJob)
    {
        CreateTourist(addedJob);
    }

    public override void JobCompleted(Job completedJob)
    {
        return;
    }

    public override void JobFailed(Job failedJob)
    {
        foreach(TourDeliveryZone tourDeliveryZone in tourDeliveryZones)
        {
            if(tourDeliveryZone.job == failedJob)
            {
                tourDeliveryZone.tourist.gameObject.SetActive(false);
            }
        }
    }

    void Start()
    {
        tourDeliveryZones.AddRange(GetComponentsInChildren<TourDeliveryZone>());
        foreach(TourDeliveryZone tourDeliveryZone in tourDeliveryZones){
            tourDeliveryZone.gameObject.SetActive(false);
        }
    }

    private Tourist CreateTourist(Job job)
    {
        Tourist tourist = GameObject.Instantiate(touristPrefab,this.transform);

        List<TourDeliveryZone> openDeliverySpots = tourDeliveryZones.Where(tourDeliveryZone => tourDeliveryZone.tourist == null).ToList();
        int spot = Random.Range(0,openDeliverySpots.Count);
        TourDeliveryZone destination = openDeliverySpots[spot];

        tourist.tourDeliveryZone = destination;
        destination.tourist = tourist;
        destination.job = job;
        touristWaitingArea.AssignTouristToSpawnPoint(tourist);

        return tourist;
    }
}
