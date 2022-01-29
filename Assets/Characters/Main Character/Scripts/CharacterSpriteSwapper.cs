using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
public class CharacterSpriteSwapper : MonoBehaviour
{
    [SerializeField]
    private SpriteLibraryAsset lightCharacter;

    [SerializeField]
    private SpriteLibraryAsset darkCharacter;

    [SerializeField]
    private SpriteLibrary library;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        library.spriteLibraryAsset = WorldController.lightCharacterShowing ? lightCharacter : darkCharacter;
    }
}
