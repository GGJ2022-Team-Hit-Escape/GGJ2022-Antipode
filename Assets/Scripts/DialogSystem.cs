using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogSystem : MonoBehaviour
{

    private List<DialogMessage> dialogQueue = new List<DialogMessage>();

    private static DialogSystem instance;

    [SerializeField]
    TextMeshProUGUI textDisplay;

    private enum DisplayStates { Waiting, Printing, WaitingForConfirmation}

    private DisplayStates displayState = DisplayStates.Waiting;

    private float printProgress = 0;

    [SerializeField]
    private InputActionReference continueKey;

    [SerializeField]
    private Animator animator;

    public static bool isCurrentlyTalking { get { return instance.displayState != DisplayStates.Waiting; } }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        continueKey.action.Enable();
    }


    private void Update()
    {
        switch (displayState)
        {
            case DisplayStates.Waiting:
                if (dialogQueue.Count != 0)
                {
                    if (dialogQueue[0] == null) // handling null dialogs
                        dialogQueue.RemoveAt(0);
                    else // starting the dialog
                    {
                        displayState = DisplayStates.Printing;
                        printProgress = 0;
                        textDisplay.text = dialogQueue[0].message;
                    }

                }
                else
                {
                    animator.SetInteger("State", 0);
                }
                break;
            case DisplayStates.Printing:

                animator.SetInteger("State", 1);

                if (dialogQueue[0].time <= 0) // then just print immediately
                    printProgress = 1;
                else
                    printProgress = Mathf.MoveTowards(printProgress, 1f, Time.deltaTime / dialogQueue[0].time);

                textDisplay.maxVisibleWords = (int)Mathf.Lerp(0, dialogQueue[0].wordCount, printProgress);

                if (continueKey.action.triggered)
                {
                    if (textDisplay.maxVisibleWords < dialogQueue[0].wordCount) // then just finish printing
                        textDisplay.maxVisibleWords = dialogQueue[0].wordCount;
                }

                if (textDisplay.maxVisibleWords >= dialogQueue[0].wordCount)
                    displayState = DisplayStates.WaitingForConfirmation;

                break;
            case DisplayStates.WaitingForConfirmation:

                animator.SetInteger("State", 2);

                if (continueKey.action.triggered)
                {
                    dialogQueue.RemoveAt(0);
                    textDisplay.text = "";
                    displayState = DisplayStates.Waiting;
                }
                break;
        }
    }


    public static void QueueDialog(DialogMessage message)
    {
        instance.dialogQueue.Add(message);
    }
}
