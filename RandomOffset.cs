using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class RandomOffset : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
        AnimatorControllerPlayable controller)
    {
        animator.SetFloat("Offset", Random.Range(0, 1f));
        base.OnStateEnter(animator, stateInfo, layerIndex, controller);
    }
}