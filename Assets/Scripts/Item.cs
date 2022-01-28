using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [SerializeField]
    private string name;

    [SerializeField]
    private Sprite itemSprite;
}
