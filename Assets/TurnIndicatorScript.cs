using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnIndicatorScript : MonoBehaviour
{
    [SerializeField]
    private GameObject trainObject;
    private TrainScript train;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        train = trainObject.GetComponent<TrainScript>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Direction", (float)train.GetDirection());
    }
}
