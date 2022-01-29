using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionTrigger : Trigger
{
    protected override string icon => "Interact";

    [SerializeField]
    private InputActionReference interactKey;

    [SerializeField]
    private float interactionDistance;

    [SerializeField]
    private CanvasGroup promptCanvasGroup;

    [SerializeField]
    private AnimationCurve promptFadeCurve;

    public static InteractionTrigger interactionTriggerInRange;

    private void Start()
    {
        interactKey?.action.Enable();
    }

    private void OnEnable()
    {
        interactKey.action.performed += InteractHit;
    }

    private void OnDisable()
    {

        interactKey.action.performed -= InteractHit;
        promptCanvasGroup.alpha = 0;
        if (interactionTriggerInRange == this)
            interactionTriggerInRange = null;
    }
    private void InteractHit(InputAction.CallbackContext obj)
    {
        if (Vector3.Distance(MainCharacter.instance.transform.position, this.transform.position) < interactionDistance)
            Run();
    }

    private void Update()
    {
        float distance = Vector3.Distance(MainCharacter.instance.transform.position, this.transform.position);
        float fade = 1f - Mathf.Clamp01(Mathf.InverseLerp(0, interactionDistance, distance));
        promptCanvasGroup.alpha = promptFadeCurve.Evaluate( fade);

        bool inRange = Vector3.Distance(MainCharacter.instance.transform.position, this.transform.position) < interactionDistance;
        if (inRange && interactionTriggerInRange == null)
            interactionTriggerInRange = this;
        else if (!inRange && interactionTriggerInRange == this)
            interactionTriggerInRange = null;
    }

    protected override void DoExtraGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, interactionDistance);
    }

}
