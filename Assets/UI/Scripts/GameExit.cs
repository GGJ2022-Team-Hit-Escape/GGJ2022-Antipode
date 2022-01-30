using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameExit : MonoBehaviour
{
    [SerializeField]
    private InputActionReference exitButton;

    // Start is called before the first frame update
    void Start()
    {
        exitButton.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (exitButton.action.triggered)
            Application.Quit();
    }
}
