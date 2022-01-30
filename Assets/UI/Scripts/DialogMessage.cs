using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogMessage
{
    public string message;
    public float time;
    public UnityEvent onMessageFinished;
    public int wordCount { get { return message.Count(x => x == ' ') + 1; } }

    public DialogMessage(string message, float time)
    {
        this.message = message ?? throw new ArgumentNullException(nameof(message));
        this.time = time;
    }
}
