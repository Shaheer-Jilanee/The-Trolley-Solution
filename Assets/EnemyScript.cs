using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private int value = 1;


    //Audio setup 
    [HideInInspector]
    public AudioSource EnemySFX;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        EnemySFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        EnemySFX.Play();
        animator.SetBool("Dead", true);
        

    }

    public void AnimationEnd()
    {
        Destroy(this);
        
    }

    public int GetValue()
    {
        return value;
    }
}
