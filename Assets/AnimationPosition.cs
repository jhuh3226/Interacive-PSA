using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPosition : StateMachineBehaviour
{
    public bool walkingAnimationFinished = false;

    override public void OnStateExit(Animator animator,AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Stop Walking"))
        {
            Debug.Log("walking animation stopped");
            walkingAnimationFinished = true;
        }
    }
}
