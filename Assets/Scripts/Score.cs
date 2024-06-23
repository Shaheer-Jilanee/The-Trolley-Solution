using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    
    
    public Text MyScoreText;
    private int ScoreNumber;

    // Start is called before the first frame update
    void Start()
    {
        ScoreNumber = 0;
        MyScoreText.text = "Score: " + ScoreNumber;
    }

    private void OnTriggerEnter2D(Collider2D traincollision)
    {
        if (traincollision.tag == "Enemy")
        {
            ScoreNumber+= 9548;
            Destroy(traincollision.gameObject);
            MyScoreText.text = "Score: " + ScoreNumber;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
