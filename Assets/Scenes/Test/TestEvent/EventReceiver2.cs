using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventReceiver2
{
    public EventReceiver2(EventTrigger trigger)
    {
        trigger.keyDown += new EventTrigger.TrigPress(OnMousePress);
    }

    void OnMousePress(Vector3 pos)
    {
        Debug.Log("OnMousePress 2");
    }

}