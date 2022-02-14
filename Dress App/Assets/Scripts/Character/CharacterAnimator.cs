using System;
using System.Collections.Generic;
using UnityEngine;

//Bare-bones Animator, it should be fine for this type of application.
public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private string[] animationTriggers = Array.Empty<string>();
    private List<int> triggerHashes = new List<int>();
    private Animator animator;
    private int animationIndex;

    public bool CanAnimate { get; set; }

    private void Awake()
    {
        CanAnimate = true;
        animator = GetComponent<Animator>();

        foreach(string trigger in animationTriggers)
        {
            triggerHashes.Add(Animator.StringToHash(trigger));
        }

        animationIndex = 0;
        PlayAnimation(animationIndex);
    }

    // Note: Consider cycling through a list of triggers hashes to play different animations.
    public void PlayAnimation()
    {
        if (!CanAnimate)
            return;

        animationIndex = (animationIndex + 1) % triggerHashes.Count;
        PlayAnimation(animationIndex);
    }

    public void ResetAnimation()
    {
        animationIndex = 0;
        PlayAnimation(animationIndex);
    }

    private void PlayAnimation(int index)
    {
        animator.SetTrigger(triggerHashes[index]);
    }
}
