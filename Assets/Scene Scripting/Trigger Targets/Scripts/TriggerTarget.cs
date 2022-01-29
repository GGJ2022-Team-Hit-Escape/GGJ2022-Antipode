using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerTarget : MonoBehaviour
{
    protected virtual string icon { get; }
    public abstract void Trigger();

    private void OnDrawGizmos()
    {

        if (icon != null)
            Gizmos.DrawIcon(this.transform.position, icon);
    }
}
