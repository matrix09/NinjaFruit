using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour {

    public delegate void TrigPress(Vector3 pos);

    public event TrigPress keyDown;

    EventReceiver1 er1;
    EventReceiver2 er2;
    private void Start()
    {
         er1 = new EventReceiver1(this);
         er2 = new EventReceiver2(this);
    }


    // Update is called once per frame
    void Update () {
        
        if(Input.GetMouseButtonDown (0))
        {
            keyDown(Input.mousePosition);
        }        
        		
	}
}
