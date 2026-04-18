using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class HookLogic
{



    /*
    void hookLogicUpdate()
    {

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            shootHook();

        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {

            Destroy(hook);
            isDestroyed = true;

        }

        if (hook != null)
        {
            Hook hookScript = hook.GetComponent<Hook>();

            if (hookScript.isAttached)
            {
                float distance = Vector3.Distance(player.position, hook.transform.position);
                playerRb.useGravity = false;
                if (distance > 2)
                {
                    player.position = Vector3.MoveTowards(player.position, hook.transform.position, pullSpeed * Time.deltaTime);
                }
            }
        }
        if (isDestroyed)
        {
            playerRb.useGravity = true;
            isDestroyed = false;
        }

    }
    */
}
