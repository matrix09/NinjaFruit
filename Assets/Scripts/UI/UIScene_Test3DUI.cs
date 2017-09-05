using UnityEngine;

public class UIScene_Test3DUI : MonoBehaviour {

    public UISprite m_UISprite;


    float GetSpriteDepth ()
    {

        //获取图片宽度和高度

        //摄像机设置的是以高度为准
        Camera cam = NGUITools.FindCameraForLayer(gameObject.layer);
        float tanv = Mathf.Tan(Mathf.Deg2Rad * cam.fieldOfView * 0.5f);
        float z = ((float)m_UISprite.height/2.0f) / tanv;

        return z;
    }

	// Use this for initialization
	void Start () {
        //float z = GetSpriteDepth();
        //Vector3 v = transform.localPosition;
        //transform.localPosition = new Vector3(v.x, v.y, z);


        //SetGameObject(gameObject);
        //GameObject obj = Instantiate(gameObject);
        //GameObject obj1 = obj;

        //float fnum = 0;
        //SetSpriteNum(ref fnum);
        //Debug.Log(fnum);

        float fnum1;
        SetSpriteValue(out fnum1);
    }

    void SetSpriteValue(out float num)
    {
        num = 1;

    }
    void SetSpriteNum (ref float num)
    {
        num = 3;
    }
    void SetGameObject (GameObject obj)
    {
        obj.name = "123";
    }


	// Update is called once per frame
	void Update () {
		
	}
}
