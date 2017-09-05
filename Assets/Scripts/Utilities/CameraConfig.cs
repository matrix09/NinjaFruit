using UnityEngine;
using System.Collections;

public class CameraConfig : MonoBehaviour {

    private void Awake()
    {
        GameObject obj = transform.GetChild(0).gameObject;

        UISprite uisp = obj.transform.GetChild(0).GetComponent<UISprite>();

        float z = GlobalHelper.GetFullScreenDepth(uisp);

        Vector3 v = obj.transform.localPosition;

        obj.transform.localPosition = new Vector3(v.x, v.y, z);
    }

    // Use this for initialization
    void Start () {
        Destroy(this);	
	}
	

}
