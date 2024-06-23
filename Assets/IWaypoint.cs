using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaypoint
{
    public void OnPass(float angle, float speed, TrainScript train);
}
