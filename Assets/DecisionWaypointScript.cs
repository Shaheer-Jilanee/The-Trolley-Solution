using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionWaypointScript : MonoBehaviour, IWaypoint
{
    [SerializeField]
    List<Transform> leftWaypoints;
    [SerializeField]
    List<Transform> rightWaypoints;
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
        if (train.GetDirection() == TrainScript.Direction.Left)
        {
            train.AddWaypointList(leftWaypoints);
        }
        else if (train.GetDirection() == TrainScript.Direction.Right)
        {
            train.AddWaypointList(rightWaypoints);
        }
        train.ResetDirection();
    }
}
