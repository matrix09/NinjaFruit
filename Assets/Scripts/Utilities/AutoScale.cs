using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScale : MonoBehaviour {

    public float m_fHeight = 640f;
    public float m_fWidth = 1136f;

	// Use this for initialization
	void Start () {

        float fsw = Screen.width;
        float fsh = Screen.height;

        float sr = fsh / fsw;

        float initr = m_fHeight / m_fWidth;

        if(sr > initr)
        {
            transform.localScale *= (initr/sr);
        }



	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
