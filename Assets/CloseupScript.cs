using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseupScript : MonoBehaviour
{
    [SerializeField]
    private GameObject train;
    private Animator trainAnimator;
    [SerializeField]
    private SpriteArrayScriptableObject closeupSprites;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        trainAnimator = train.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(trainAnimator.GetBool("Bonked") == true)
        {
            image.sprite = closeupSprites.sprites[3];
        }
        else if(trainAnimator.GetBool("Derailed") == true)
        {
            image.sprite = closeupSprites.sprites[2];
        }
        else if(trainAnimator.GetFloat("Speed") > 5)
        {
            image.sprite = closeupSprites.sprites[1];
        }
        else
        {
            image.sprite = closeupSprites.sprites[0];
        }
    }
}
