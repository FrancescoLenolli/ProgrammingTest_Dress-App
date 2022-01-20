using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;
    private int walkTriggerHash;
    private int tPoseTriggerHash;

    public bool CanAnimate { get; set; }

    private void Awake()
    {
        CanAnimate = true;
        animator = GetComponent<Animator>();

        // Store the value of the animation triggers for better performance.
        walkTriggerHash = Animator.StringToHash("Walking");
        tPoseTriggerHash = Animator.StringToHash("T-Pose");
    }

    // Note: Consider cycling through a list of triggers hashes to play different animations.
    public void PlayAnimation()
    {
        if (!CanAnimate)
            return;

        animator.SetTrigger(walkTriggerHash);
    }

    public void ResetAnimation()
    {
        animator.SetTrigger(tPoseTriggerHash);
    }
}
