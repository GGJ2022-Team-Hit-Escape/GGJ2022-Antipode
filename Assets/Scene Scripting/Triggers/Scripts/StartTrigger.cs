using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : Trigger
{
    protected override string icon => "Play";

    // Start is called before the first frame update
    void Start()
    {
        this.Run();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
