using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISegment
{
    public void Rotate(int amount);
    public IWaypoint GetStart();
}
