using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Switcher : MonoBehaviour
{
    [SerializeField]
    private GameObject lightGameObject;

    [SerializeField]
    private GameObject darkGameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lightGameObject?.SetActive(WorldController.lightWorldShowing);
        darkGameObject?.SetActive(!WorldController.lightWorldShowing);
    }
}
