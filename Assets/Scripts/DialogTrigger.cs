using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : TriggerTarget
{
    protected override string icon => "Dialog";

    [SerializeField]
    private List<DialogMessage> dialog = new List<DialogMessage>();

    public override void Trigger()
    {
        foreach (var message in dialog)
            DialogSystem.QueueDialog(message);
    }
}
