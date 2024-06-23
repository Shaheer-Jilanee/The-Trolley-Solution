using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageScript : MonoBehaviour
{

    private List<Transform> waypoints = new List<Transform>();
    private float baseSpeed;
    private float boostAccel;
    private float currentSpeed;
    private float speedLimit;
    private float brakeAccel;
    private Vector2 moveVector = new Vector2(1, 0);
    public float moveAngle = 0;
    private Animator animator;
    private int id;
    private int trainCarriages;
    private Transform aheadCarriage;
    public bool dead = false;
    private Vector2 prevMoveVector = new Vector2(0, 0);
    //private float spread;
    //private float catchupSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if (waypoints.Count > 0)
            {
                Transform target = waypoints[0];

                moveVector = (target.position - transform.position).normalized;
                prevMoveVector = moveVector;
                moveAngle = Vector2.SignedAngle(Vector2.right, moveVector);
                animator.SetFloat("Angle", moveAngle);
                float orderingFactor = moveAngle < 0 ? -trainCarriages + id : -2 - id;
                /*float currentSpread = Vector2.Distance(aheadCarriage.position, transform.position);
                int spreadStatus = 0;
                if(currentSpread > spread)
                {
                    spreadStatus = 1;
                }
                else if(currentSpread < spread)
                {
                    spreadStatus = -1;
                }
                currentSpeed += catchupSpeed * Time.deltaTime * 0.5f * spreadStatus;*/
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    currentSpeed += boostAccel * Time.deltaTime * 0.5f;
                    if (currentSpeed > speedLimit)
                    {
                        currentSpeed = speedLimit;
                    }
                    transform.position = Vector2.MoveTowards(transform.position, target.position, currentSpeed * Time.deltaTime);
                    currentSpeed += boostAccel * Time.deltaTime * 0.5f;
                    if (currentSpeed > speedLimit)
                    {
                        currentSpeed = speedLimit;
                    }
                }
                else
                {
                    currentSpeed -= brakeAccel * Time.deltaTime * 0.5f;
                    if (currentSpeed < baseSpeed)
                    {
                        currentSpeed = baseSpeed;
                    }
                    transform.position = Vector2.MoveTowards(transform.position, target.position, currentSpeed * Time.deltaTime);
                    currentSpeed -= brakeAccel * Time.deltaTime * 0.5f;
                    if (currentSpeed < baseSpeed)
                    {
                        currentSpeed = baseSpeed;
                    }
                }
                //currentSpeed -= catchupSpeed * Time.deltaTime * 0.5f * spreadStatus;



                if ((Vector2)transform.position == (Vector2)target.position)
                {
                    waypoints.RemoveAt(0);
                }
                transform.position += new Vector3(0, 0, orderingFactor);
            }
        }
        //Dead
        else
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= brakeAccel * Time.deltaTime * 0.5f;
                if (currentSpeed < 0)
                {
                    currentSpeed = 0;
                }
                transform.position = (Vector3)Vector2.MoveTowards(transform.position, transform.position + (Vector3)prevMoveVector, currentSpeed * Time.deltaTime) + new Vector3(0, 0, transform.position.z);
                currentSpeed -= brakeAccel * Time.deltaTime * 0.5f;
                if (currentSpeed < 0)
                {
                    currentSpeed = 0;
                }
            }
        }
    }

    public void AddWaypoint(Transform waypoint)
    {
        waypoints.Add(waypoint);
    }
    public void AddWaypointList(IEnumerable<Transform> waypointList)
    {
        waypoints.AddRange(waypointList);
    }
    public void SetValues(float baseSpeed, float boostAccel, float speedLimit, float brakeAccel, int id, int trainCarriages, Transform aheadCarriage)
    {
        this.baseSpeed = baseSpeed;
        this.boostAccel = boostAccel;
        this.speedLimit = speedLimit;
        this.brakeAccel = brakeAccel;
        this.id = id;
        this.trainCarriages = trainCarriages;
        this.aheadCarriage = aheadCarriage;
        //this.spread = spread;
        //this.catchupSpeed = catchupSpeed;
    }
    public void Derail()
    {
        dead = true;
    }
    public void Hit(float damage)
    {
        currentSpeed -= damage;
        if (currentSpeed <= 0)
        {
            currentSpeed = damage;
            prevMoveVector *= -1;
            dead = true;

        }
    }

}
