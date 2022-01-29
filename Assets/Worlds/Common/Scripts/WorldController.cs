using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class WorldController : MonoBehaviour
{

    [SerializeField]
    private TilemapRenderer baseWorldRenderer;

    [SerializeField]
    private bool _lightWorldShowing = true;

    public static bool lightWorldShowing { get { return instance._lightWorldShowing; } }

    [SerializeField]
    private bool _lightCharacterShowing = false;
    public static bool lightCharacterShowing { get { return instance._lightCharacterShowing; } }

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

    private MaterialPropertyBlock block;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        block = new MaterialPropertyBlock();
    }

    // Update is called once per frame
    void Update()
    {
        if (block == null)
            block = new MaterialPropertyBlock();
        baseWorldRenderer.GetPropertyBlock(block);
        block.SetFloat("_Mix", _lightWorldShowing ? 0f: 1f);
        baseWorldRenderer.SetPropertyBlock(block);
    }
}
