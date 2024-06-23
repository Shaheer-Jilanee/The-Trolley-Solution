using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentScript : MonoBehaviour
{
    [SerializeField]
    private Transform[] ends;
    [SerializeField]
    private Transform start;
    private GameObject previousSegment;
    private int depth = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rotate(int amount)
    {
        foreach (Transform end in ends)
        {
            TransitionWaypointScript transition = end.gameObject.GetComponent<TransitionWaypointScript>();
            //if (waypoint is TransitionWaypointScript)
            //{

                //TransitionWaypointScript transition = (TransitionWaypointScript)waypoint;
                //transition.ResetDirection();
            transition.Rotate(amount);
            //}
        }
    }

    public Transform GetStart()
    {
        return start;
    }
    public void SetPrevious(GameObject previousSegment, int depth)
    {
        this.previousSegment = previousSegment;
        this.depth = depth;
        if(this.depth > 1)
        {
            SegmentScript previousScript = previousSegment.GetComponent<SegmentScript>();
            previousScript.DestroyPrevious();
            this.depth--;
        }
    }
    public void DestroyPrevious()
    {
        Destroy(previousSegment);
        depth--;
    }
    public int GetDepth()
    {
        return depth;
    }
}
