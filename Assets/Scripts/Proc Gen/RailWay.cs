using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailWay : MonoBehaviour
{
    
    RailWaySpawner railwayspawner;
    // Start is called before the first frame update
    private void Start()
    {
        railwayspawner = GameObject.FindObjectOfType<RailWaySpawner>();
    }


    private void OnTriggerExit (Collider other)
    {
        railwayspawner.SpawnRailSegment();
        Destroy(gameObject, 5);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
