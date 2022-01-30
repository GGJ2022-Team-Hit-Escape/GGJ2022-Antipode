using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationTarget : TriggerTarget
{
    [SerializeField]
    private string propertyName;

    private Animator animator;

    [SerializeField]
    private bool state = false;

    protected override string icon => "Animated";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(propertyName, state);
    }

    public override void Trigger()
    {
        state = !state;
        animator.SetBool(propertyName, state);
    }
}
