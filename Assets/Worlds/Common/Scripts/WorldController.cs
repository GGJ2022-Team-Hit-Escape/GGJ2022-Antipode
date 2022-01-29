using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldController : MonoBehaviour
{

    [SerializeField]
    private GameObject lightWorld;

    [SerializeField]
    private GameObject darkWorld;

    [SerializeField]
    private TilemapRenderer baseWorldRenderer;

    [SerializeField]
    private bool lightWorldShowing = true;

    private MaterialPropertyBlock block;

    // Start is called before the first frame update
    void Start()
    {
        block = new MaterialPropertyBlock();
    }

    // Update is called once per frame
    void Update()
    {
        lightWorld.SetActive(lightWorldShowing);
        darkWorld.SetActive(!lightWorldShowing);

        baseWorldRenderer.GetPropertyBlock(block);
        block.SetFloat("_Mix", lightWorldShowing ? 0f: 1f);
        baseWorldRenderer.SetPropertyBlock(block);
    }
}
