using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueWaypointScript : MonoBehaviour, IWaypoint
{
    [SerializeField]
    List<Transform> nextWaypoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPass(float angle, float speed, TrainScript train)
    {
        train.AddWaypointList(nextWaypoints);
    }
}
