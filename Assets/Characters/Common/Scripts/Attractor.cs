using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    private static Attractor _instance;

    public static Attractor instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<Attractor>();
            return _instance;
        }
    }

    private void Update()
    {
        
    }
}
