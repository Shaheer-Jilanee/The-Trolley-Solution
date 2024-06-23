using UnityEngine;

public class RailWaySpawner : MonoBehaviour
{

    //The segments to be spawned
    public GameObject Segment1;
    public GameObject Segment2;


    //Where the collision will occur to spawn the next platform
    public GameObject SpawnNewSegmentS1;
    public GameObject SpawnNewSegmentS2;
    

    Vector3 OldSpawnPoint1;
    Vector3 OldSpawnPoint2;

    Vector3 nextSpawnPoint1;
    Vector3 nextSpawnPoint2;

    // 0 for seg 1, 1 for seg 2
    public int spawnIndex;

    public void SpawnRailSegment()
    {
        
        GameObject temp = Instantiate(Segment1, nextSpawnPoint1, Quaternion.identity);
        nextSpawnPoint1 = temp.transform.GetChild(0).transform.position;

    }


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnRailSegment();
        }
    }

   

}


