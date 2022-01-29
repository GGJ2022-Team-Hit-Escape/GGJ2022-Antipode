using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger : MonoBehaviour
{
    [SerializeField]
    private List<TriggerTarget> onTrigger = new List<TriggerTarget>();

    [SerializeField]
    private UnityEngine.Events.UnityEvent onTriggerEvents;

    [SerializeField]
    private bool disableOnComplete = false;

    protected virtual string icon { get; } 

    protected void Run()
    {
        foreach (var trigger in onTrigger)
            trigger?.Trigger();

        onTriggerEvents?.Invoke();

        if (disableOnComplete)
            enabled = false;
    }

    private void OnDrawGizmos()
    {
        foreach(var trigger in onTrigger)
        {
            if (trigger != null)
                Gizmos.DrawLine(this.transform.position, trigger.gameObject.transform.position);
        }

        if (icon != null)
            Gizmos.DrawIcon(this.transform.position, icon);

        DoExtraGizmos();
    }

    protected virtual void DoExtraGizmos()
    {

    }

}
