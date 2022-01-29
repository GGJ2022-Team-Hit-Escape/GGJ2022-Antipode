using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : TriggerTarget
{
    [SerializeField]
    private Item itemToSpawn;

    protected override string icon => "Item Spawn";

    public override void Trigger()
    {
        if (itemToSpawn != null)
        {
            GameObject spawnedObject = GameObject.Instantiate(itemToSpawn.gameObject);
            spawnedObject.transform.position = this.transform.position;
        }
    }
}
