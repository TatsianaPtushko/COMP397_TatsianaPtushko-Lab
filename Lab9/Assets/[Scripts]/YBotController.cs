using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YBotController : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            anim.SetInteger("AnimationState", 0); //Idle animation
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetInteger("AnimationState", 1); //walk animation
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetInteger("AnimationState", 2); //Hook animation
        }
        

    }
}
