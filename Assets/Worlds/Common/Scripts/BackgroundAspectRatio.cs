using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAspectRatio : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer backgroundRenderer;

    [SerializeField]
    private Vector2Int referenceDimensions = new Vector2Int(512,512);

    MaterialPropertyBlock block;
    // Start is called before the first frame update
    void Start()
    {
        block = new MaterialPropertyBlock();   
    }

    // Update is called once per frame
    void Update()
    {
        backgroundRenderer.GetPropertyBlock(block);
        Vector2Int screenDimensions = new Vector2Int(Screen.width, Screen.height);
        Vector2 fitRatio = CalculateFitRatio(referenceDimensions, screenDimensions);
        block.SetVector("_AspectScalar", fitRatio);
        backgroundRenderer.SetPropertyBlock(block);
    }

    private Vector2 CalculateFitRatio (Vector2Int image, Vector2Int container)
    {
        float imageAspect = (float)image.x / image.y;
        float containerAspect = (float)container.x / container.y;

        if (imageAspect > containerAspect)//Then image is squished horizontally, need to expand width to display properly
        {
            float requiredWidth = (float)container.y * imageAspect;
            float actualWidth = container.x;
            float multiplyer = actualWidth/requiredWidth;
            return new Vector2(multiplyer, 1);
        }
        else //Then image is squished vertically, need to expand height to display properly
        {
            float requiredHeight = (float)container.x / imageAspect;
            float actualHeight = container.y;
            float multiplyer = actualHeight/requiredHeight;
            return new Vector2(1, multiplyer);
        }
    }
}
