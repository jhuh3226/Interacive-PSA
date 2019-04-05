using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHolder : MonoBehaviour
{
    // This var will determine if the animation is started
    public bool animation_started = false;
    // This var will determine if the animation is finished
    public bool animation_finished = true;

    public GameObject animator;

    void Update()
    {

        //// if user triggers Fire1
        //if (Input.GetButtonUp('Fire1'))
        //{

        //    // initialize the flags
        //    animation_started = true;
        //    animation_finished = false;

        //    // Start the animation
        //    // this animation moves the box from local X = 0 to X = 10 using a curve to deaccelerate
        //    animation.Play("boxanim");
        //}

    }

    /* This function is trigger at the end of the animation */
    public void animationFinished()
    {
        animation_finished = true;
    }

    /*  
        At the end of the frame if the animation is finished
        we update the position of the parent to the last position of the child
        and set the position of the child to zero inside the parent.
    */
    void LateUpdate()
    {
        // if the animation is finished and it was started
        if (animation_finished && animation_started)
        {
            // set the flag
            animation_started = false;
            // update the parent position
            //transform.parent.position = transform.position;
            animator.transform.parent.position = animator.transform.position;
            // update the box position to zero inside the parent
            transform.localPosition = Vector3.zero;
        }
    }
}
