using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField]
    private float damage = 1;
    Animator animator;
    public bool dead = false;

    //Audio setup 
    [HideInInspector]
    public AudioSource ObstacleSFX;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        ObstacleSFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float GetDamage()
    {
        return damage;
    }
    public void Die()
    {
        dead = true;
        animator.SetBool("Dead", true);
        ObstacleSFX.Play();

    }

    void AnimationEnd()
    {
        Destroy(this);
    }
}
