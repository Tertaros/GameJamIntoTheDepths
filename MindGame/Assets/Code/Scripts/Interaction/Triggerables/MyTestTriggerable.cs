using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTestTriggerable : TriggerableItem
{
    public override void OnTriggered()
    {
        Debug.Log("I am triggered! [" + gameObject.name + "]");
    }
}
