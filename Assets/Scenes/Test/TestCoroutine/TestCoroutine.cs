using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoroutine : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Test());
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Update");
	}

    IEnumerator Test ()
    {
        Debug.Log("Test");
        yield return new WaitForEndOfFrame();
        Debug.Log("Test After frame");
        yield return null;
        Debug.Log("Test After null");
        yield return new WaitForSeconds(3);
        Debug.Log("Test After 3 seconds");
    }

}
