using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUpdatesV2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Update 2");
    }

    void LateUpdate()
    {
        Debug.Log("LateUpdate 2");
    }
}
