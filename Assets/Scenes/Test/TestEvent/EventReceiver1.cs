using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventReceiver1
{
    public EventReceiver1 (EventTrigger trigger)
    {
        trigger.keyDown += new EventTrigger.TrigPress(OnMousePress);
    }

    void OnMousePress (Vector3 pos)
    {
        Debug.Log("OnMousePress 1");
    }

}

public class EventReceiver3
{
    public EventReceiver3(EventTrigger trigger)
    {
        trigger.keyDown += new EventTrigger.TrigPress(OnMousePress);
    }

    void OnMousePress(Vector3 pos)
    {
        Debug.Log("OnMousePress 1");
    }

}