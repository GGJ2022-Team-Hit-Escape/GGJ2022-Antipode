using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{

    [SerializeField]
    private InputActionReference actionButton;


    [SerializeField]
    private int nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        actionButton.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (actionButton.action.triggered)
            SceneManager.LoadScene(nextLevel);
    }
}
