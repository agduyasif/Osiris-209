using UnityEngine.SceneManagement;
using UnityEngine;


public class Death : Trigger
{

    protected override void OnEnter(Collider other)
    {
        ResetScene.Reset();
    }

   
}
