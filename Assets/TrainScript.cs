using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrainScript : MonoBehaviour
{
    [SerializeField]
    private List<Transform> waypoints = new List<Transform>();

    [SerializeField]
    private int carriageCount = 3;

    [SerializeField]
    private GameObject carriagePrefab;

    private List<CarriageScript> carriages = new List<CarriageScript>();

    [SerializeField]
    private float carriageSpread = 1;

    [SerializeField]
    private float firstCarriageSpread = 1;

    [SerializeField]
    private float baseSpeed = 2;

    [SerializeField]
    private float boostAccel = 1;

    private float currentSpeed;

    [SerializeField]
    private float speedLimit = 10;

    [SerializeField]
    private float brakeAccel = 3;

    //[SerializeField]
    //private float rigidity = 1;

    //[SerializeField]
    //private float damping = 0.99f;

    //[SerializeField]
    //private float catchupSpeed = 0.1f;

    private Vector2 moveVector = new Vector2(1,0);

    [SerializeField]
    public float moveAngle = 0;

    private Animator animator;

    [SerializeField]
    private Transform pivot;

    //private Transform camera;


    public Text MyScoreText;
    private int ScoreNumber;
    [SerializeField]
    private GameObject audioManager;
    private AudioSource[] audioSources;

    [SerializeField]
    TextMeshProUGUI highScoreText;

    [SerializeField]
    TextMeshProUGUI speedText;

    public ButtonManager buttonManager;


   

    [SerializeField]
    private float speedMultiplier = 2;
    [SerializeField]
    private float timeMultiplier = 0.01f;
    public bool dead = false;
    private Vector2 prevMoveVector = new Vector2(0,0);
    private float elapsedTime = 0f;

    //private Transform movePointer;

    public enum Direction
    {
        None,
        Left,
        Right
    }
    private Direction direction = Direction.None;

    // Start is called before the first frame update
    void Start()
    {
        //movePointer = transform.Find("MovePointer");
        currentSpeed = baseSpeed;
        audioSources = audioManager.GetComponents<AudioSource>();
        transform.position = waypoints[0].position;
        for (int i = 0; i < carriageCount; i++)
        {
            GameObject newCarriage = Instantiate(carriagePrefab);
            newCarriage.transform.SetParent(transform.parent);
            Transform aheadCarriage;
            //float desiredSpread;
            if (i == 0)
            {
                newCarriage.transform.position = transform.position - new Vector3(0, firstCarriageSpread, 0);
                aheadCarriage = this.transform;
                //desiredSpread = firstCarriageSpread;
            }
            else
            {
                newCarriage.transform.position = transform.position - new Vector3(0, carriageSpread * i + firstCarriageSpread, 0);
                aheadCarriage = carriages[i - 1].transform;
                //desiredSpread = carriageSpread;
            }
            
            CarriageScript newScript = newCarriage.GetComponent<CarriageScript>();
            newScript.AddWaypointList(waypoints);
            newScript.SetValues(baseSpeed, boostAccel, speedLimit, brakeAccel, i, carriageCount, aheadCarriage);
            carriages.Add(newScript);
        }
        waypoints.RemoveAt(0);
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", currentSpeed);
        //camera = transform.Find("TrainCamera");

        //Score + enemy related 

        ScoreNumber = 0;
        MyScoreText.text = "Score: " + ScoreNumber;

        UpdateHighScoreText();
    
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Direction.Left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Direction.Right;
        }

        if (!dead)
        {
            if (waypoints.Count > 0)
            {
                Transform target = waypoints[0];

                moveVector = (target.position - transform.position).normalized;
                prevMoveVector = moveVector;
                moveAngle = Vector2.SignedAngle(Vector2.right, moveVector);
                Quaternion rotation = Quaternion.identity;
                rotation.eulerAngles = new Vector3(0,0,moveAngle);
                pivot.rotation = rotation;
                float orderingFactor = moveAngle < 0 ? -carriageCount - 1 : -1;
                animator.SetFloat("Angle", moveAngle);
                if(currentSpeed < baseSpeed)
                {
                    currentSpeed += boostAccel * Time.deltaTime * 0.5f;
                    if(currentSpeed > baseSpeed)
                    {
                        currentSpeed = baseSpeed;
                    }
                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    currentSpeed += boostAccel * Time.deltaTime * 0.5f;
                    if (currentSpeed > speedLimit)
                    {
                        currentSpeed = speedLimit;
                    }
                    transform.position = Vector2.MoveTowards(transform.position, target.position, currentSpeed * (elapsedTime * timeMultiplier + 1) * Time.deltaTime);
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
                    transform.position = Vector2.MoveTowards(transform.position, target.position, currentSpeed * (elapsedTime * timeMultiplier + 1) * Time.deltaTime);

                    currentSpeed -= brakeAccel * Time.deltaTime * 0.5f;
                    if (currentSpeed < baseSpeed)
                    {
                        currentSpeed = baseSpeed;
                    }
                }
                if (currentSpeed < baseSpeed)
                {
                    currentSpeed += boostAccel * Time.deltaTime * 0.5f;
                    if (currentSpeed > baseSpeed)
                    {
                        currentSpeed = baseSpeed;
                    }
                }
                transform.position += new Vector3(0, 0, orderingFactor);
                animator.SetFloat("Speed", currentSpeed);


                //camera.localPosition = new Vector3(moveVector.x * (currentSpeed - baseSpeed), moveVector.y * (currentSpeed - baseSpeed), -10);

                //movePointer.position = new Vector3(transform.position.x + moveVector.x, transform.position.y + moveVector.y, transform.position.z);


                if ((Vector2)transform.position == (Vector2)target.position)
                {
                    IWaypoint targetScript = waypoints[0].gameObject.GetComponent<IWaypoint>();
                    targetScript.OnPass(moveAngle, currentSpeed, this);
                    waypoints.RemoveAt(0);
                }
                
            }
            //Derailed
            else
            {
                dead = true;
                animator.SetBool("Derailed", true);
                foreach(CarriageScript carriage in carriages)
                {
                    carriage.Derail();
                }
            }
        }
        //Dead
        else
        {
            if(currentSpeed > 0)
            {
                currentSpeed -= brakeAccel * Time.deltaTime * 0.5f;
                if(currentSpeed < 0)
                {
                    currentSpeed = 0;
                }
                transform.position = (Vector3)Vector2.MoveTowards(transform.position, transform.position + (Vector3)prevMoveVector, currentSpeed * Time.deltaTime) + new Vector3(0,0,transform.position.z);
                currentSpeed -= brakeAccel * Time.deltaTime * 0.5f;
                if (currentSpeed < 0)
                {
                    currentSpeed = 0;
                }
            }
        }
        //Death Screen Logic
        if (dead == true)
        {
            buttonManager.gameOver();
        }
        //Debug.Log("Train: " + currentSpeed);
        float actualSpeed = currentSpeed * (elapsedTime * timeMultiplier + 1);
        speedText.text = "Speed: " + actualSpeed.ToString("F1");
        float speedDifference = actualSpeed - baseSpeed;
        audioSources[1].pitch = 0.5f+speedDifference / 30f;
        //foreach (AudioSource source in audioSources)
        //{
        //    source.pitch = 1 + speedDifference / 50f;
        //}
    }

    public void AddWaypoint(Transform waypoint)
    {
        waypoints.Add(waypoint);
        foreach(CarriageScript carriage in carriages)
        {
            carriage.AddWaypoint(waypoint);
        }
    }
    public void AddWaypointList(IEnumerable<Transform> waypointList)
    {
        waypoints.AddRange(waypointList);
        foreach (CarriageScript carriage in carriages)
        {
            carriage.AddWaypointList(waypointList);
        }
    }
    public void ResetDirection()
    {
        direction = Direction.None;
    }
    public Direction GetDirection()
    {
        return direction;
    }

    //Score + Enemy Related 
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
            EnemyScript enemyScript = collider.gameObject.GetComponent<EnemyScript>();
            float speedDifference = currentSpeed - baseSpeed;
            ScoreNumber += enemyScript.GetValue() + Mathf.RoundToInt(speedDifference * speedMultiplier);
            enemyScript.Die();
            enemyScript.EnemySFX.Play();
            MyScoreText.text = "Score: " + ScoreNumber;

            CheckHighScore();
            //UpdateHighScoreText();
        
        }

        else if (collider.tag == "Obstacle")
        {
            ObstacleScript obstacleScript = collider.gameObject.GetComponent<ObstacleScript>();
            foreach (CarriageScript carriage in carriages)
            {
                carriage.Hit(obstacleScript.GetDamage());
            }
            float multiplier = (elapsedTime * timeMultiplier + 1);
            float actualSpeed = currentSpeed * multiplier;
            actualSpeed -= obstacleScript.GetDamage();
            if(actualSpeed <= 0)
            {
                currentSpeed = obstacleScript.GetDamage();
                prevMoveVector *= -1;
                
                dead = true;
                animator.SetBool("Bonked", true);

                
            }
            else
            {
                currentSpeed -= obstacleScript.GetDamage() / multiplier;
                obstacleScript.Die();
                //collisionsfx.Play();
            }
            
        }
    }

    void CheckHighScore()
    {
        if (ScoreNumber > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", ScoreNumber);
        }
       
    }

    void UpdateHighScoreText()
    {
        highScoreText.text = $"HighScore: {PlayerPrefs.GetInt("HighScore",0)}";

    }

    public Vector2 GetVelocityVector()
    {
        return new Vector2(Mathf.Cos(moveAngle * Mathf.Deg2Rad), Mathf.Sin(moveAngle * Mathf.Deg2Rad)) * GetVelocityFloat();
    }

    public float GetVelocityFloat()
    {
        return currentSpeed*0.003f;
    }
}



