using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class WorldController : MonoBehaviour
{


    [SerializeField]
    private bool _lightWorldShowing = true;

    public static bool lightWorldShowing { get { return instance._lightWorldShowing; } set { instance._lightWorldShowing = value; } }

    [SerializeField]
    private bool _lightCharacterShowing = false;
    public static bool lightCharacterShowing { get { return instance._lightCharacterShowing; } set { instance._lightCharacterShowing = value; } }

    private static WorldController _instance;

    private static WorldController instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<WorldController>();
            return _instance;
        }
    }

    public static GameObject LightTilemap { get => instance._lightTilemap;  }
    public static GameObject DarkTilemap { get => instance._darkTilemap; }

    [SerializeField]
    private GameObject _lightTilemap;

    [SerializeField]
    private GameObject _darkTilemap;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

}
