using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionWaypointScript : MonoBehaviour, IWaypoint
{
    [SerializeField]
    private ObjectArrayScriptableObject segments;
    //[SerializeField]
    private int direction = 0;
    [SerializeField]
    private int baseDirection = 0;

    // Start is called before the first frame update
    void Start()
    {
        //direction = baseDirection;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPass(float angle, float speed, TrainScript train)
    {
        GameObject nextSegment = Instantiate(segments.objects[Random.Range(0, segments.objects.Length)]);
        nextSegment.transform.SetParent(transform.parent.parent);
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(nextSegment.transform.rotation.eulerAngles.x, nextSegment.transform.rotation.eulerAngles.y, 90 * direction);
        nextSegment.transform.rotation = rotation;
        float xOffset = -0.5f;
        float yOffset = 1f;
        if(direction%2 == 0)
        {
            xOffset = direction == 0 ? -0.5f : 0.5f;
            yOffset = direction == 0 ? 6f : -6f;
        }
        else
        {
            xOffset = direction == 1 ? -6f : 6f;
            yOffset = direction == 1 ? -0.5f : 0.5f;
        }
        nextSegment.transform.localPosition = transform.position + new Vector3(xOffset, yOffset, 0);
        SegmentScript nextScript = nextSegment.GetComponent<SegmentScript>();
        nextScript.Rotate(direction);
        GameObject currentSegment = transform.parent.gameObject;
        SegmentScript currentScript = currentSegment.GetComponent<SegmentScript>();
        nextScript.SetPrevious(currentSegment, currentScript.GetDepth()+1);
        train.AddWaypoint(nextScript.GetStart());
    }

    //public void ResetDirection()
    //{
        
    //}

    public void Rotate(int amount)
    {
        direction = baseDirection;
        direction += amount;
        direction %= 4;
    }
}
