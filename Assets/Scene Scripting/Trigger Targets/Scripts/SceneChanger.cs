using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : TriggerTarget
{
    protected override string icon => "Exit";

    [SerializeField]
    private int nextScene;

    public override void Trigger()
    {
        SceneManager.LoadScene(nextScene);
    }

}
