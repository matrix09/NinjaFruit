using AttTypeDefine;
using System.Collections.Generic;
using UnityEngine;
public class GlobalHelper {

    public static Dictionary<eFruitType, string> g_dGlobalInfos = new Dictionary<eFruitType, string>();

    public static bool g_bIsOpenLog;


    public static void PauseGame ()
    {
        Time.timeScale = 0.0f;
    }

    public static void ContinueGame ()
    {
        Time.timeScale = 1.0f;
    }

    //登录水果旋转的速度
    public static UnityEngine.Vector3 g_vRotateSpeed = new UnityEngine.Vector3(40f, 40f, 40f);

    //关卡数据
    static public Dictionary<eSceneName, CNextSceneInfo> g_dicNextSceneInfo = new Dictionary<eSceneName, CNextSceneInfo>();

    //当前存储的下一个场景的信息
    static public CNextSceneInfo g_CurSceneInfo;

    //初始化全局数据
    public static void InitGlobalDatas ()
    {
        CNextSceneInfo _next = new CNextSceneInfo();
        _next.m_strSceneName = "MapLogin";
        _next.m_strResPath = "";
        g_dicNextSceneInfo[eSceneName.Scene_Login] = _next;

        _next = new CNextSceneInfo();
        _next.m_strSceneName = "MapGame";
        _next.m_strResPath = "Prefabs/Maps/1001";
        g_dicNextSceneInfo[eSceneName.Scene_Game] = _next;

        //初始化水果数据      
        g_dGlobalInfos[eFruitType.Fruit_Melon] = "Prefabs/Fruits/melon_whole";
        g_dGlobalInfos[eFruitType.Fruit_MelonHalf] = "Prefabs/Fruits/melon_half";
        g_dGlobalInfos[eFruitType.Fruit_MelonQuarter] = "Prefabs/Fruits/melon_quarter";
        g_dGlobalInfos[eFruitType.Fruit_Lemon] = "Prefabs/Fruits/lemon_whole";
        g_dGlobalInfos[eFruitType.Fruit_LemonHalf] = "Prefabs/Fruits/lemon_half";
        g_dGlobalInfos[eFruitType.Fruit_LemonQuarter] = "Prefabs/Fruits/lemon_quarter";
        g_dGlobalInfos[eFruitType.Fruit_Pear] = "Prefabs/Fruits/pear_whole";
        g_dGlobalInfos[eFruitType.Fruit_PearHalf] = "Prefabs/Fruits/pear_half";
        g_dGlobalInfos[eFruitType.Fruit_PearQuarter] = "Prefabs/Fruits/pear_quarter";
        g_dGlobalInfos[eFruitType.Fruit_Missle] = "Prefabs/Fruits/Missle";
    }

    //动态加载垃圾回收站
    public static void LoadGarbageCollection ()
    {
        if(null == GameObject.FindGameObjectWithTag ("GC"))
        {
            GameObject obj = new GameObject("GarbageCollection");
            obj.transform.parent = null;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.AddComponent<GarbageCollection>();
            obj.tag = "GC";
        }
    }

    //<summary>
    //功能描述动态加载背景图片，返回图片对象深度坐标，让背景图片满屏显示
    //返回类型 ： float
    //<summary>
    static public float GetFullScreenDepth(UISprite sp)
    {

        float tanFov = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2.0f);
        float y = ((float)sp.height) / 2.0f;
        float x = ((float)sp.width) / 2.0f;
        float z1 = y / tanFov;
        float z2 = x / tanFov;
        float screenrate = (float)Screen.width / (float)Screen.height;
        float picrate = x / y;

        if (picrate > 1.0f)
        {
            return  (z1 * ((float)Screen.height)/(2 * y)) * (1168f/1136f);
        }
        else
        {
            return z1;
        }
    }

    //<summary>
    //Parameter : zPos 表示当前水果的深度位置
    //Parameter : size 水果在xyz三个轴向的最大值。
    //Return Value : 表示离开摄像机视野的坐标
    //<summary>
    public static float GetVisibleYPos(float zPos, float size)
    {
        float yBtm = 0f;

        float zdis = zPos - UnityEngine.Camera.main.transform.position.z;

        float tan = UnityEngine.Mathf.Tan(UnityEngine.Mathf.Deg2Rad * UnityEngine.Camera.main.fieldOfView * 0.5f);

        yBtm = UnityEngine.Camera.main.transform.position.y -  zdis * tan - size;

        return yBtm;
    }

    //<summary>
    //Parameter : zPos 表示当前水果的深度位置
    //Parameter : size 水果在xyz三个轴向的最大值。
    //Return Value : 表示离开摄像机视野的X坐标
    //<summary>
    public static float GetVisibleXPos(float zPos, float size)
    {
        float xBtm = 0f;

        float zdis = zPos - Camera.main.transform.position.z;

        float tan = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView * 0.5f);

        float rate = (float)Screen.width / (float)Screen.height;

        xBtm = Camera.main.transform.position.x - zdis * tan * rate - size;

        return xBtm;
    }

    //<summary>
    //function : 加载新场景
    //<summary>
    public static void LoadLevel (eSceneName _scene)
    {
        //加载下一个场景
        g_CurSceneInfo = g_dicNextSceneInfo[_scene];

        //加载切换场景.
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync
            ("MapLoading");

    }

    //<summary>
    //function : 加载场景资源
    //<summary>
    public static void LoadSceneRes ()
    {

        if(g_CurSceneInfo.m_strSceneName == "MapGame")
        {
            //加载UI Root
            LoadUIRoot();

            Object[] objs = Resources.LoadAll(g_CurSceneInfo.m_strResPath);
            GameObject objLauncher = null;
            for(int i = 0; i < objs.Length; i++)
            {
                GameObject obj = Object.Instantiate(objs[i]) as GameObject;
                obj.name = objs[i].name;
                if (obj.name == "Launcher")
                    objLauncher = obj;
            }

            Launcher launcher = objLauncher.GetComponent<Launcher>();
            launcher.OnStart();

        }
    }
    
    #region UI

    static private GameObject g_UIAnchor;

    //<summary>
    //function : 设置场景背景图片
    //<summary>
    static public void SetBackImg (string name)
    {
        GameObject obj = GameObject.FindGameObjectWithTag("AnchorBack");
        UISprite backUI = obj.GetComponentInChildren<UISprite>();
        backUI.spriteName = name;
    }

    static public void LoadUIRoot()
    {
        //加载ui root
        if (!GameObject.Find("UI Root"))
        {
            Object obj = Resources.Load("Prefabs/UI/UI Root");
            GameObject obj1 = Object.Instantiate(obj) as GameObject;
            obj1.name = obj.name;
        }
        //加载游戏登录界面
        g_UIAnchor = GameObject.FindGameObjectWithTag("Anchor");

    }

    static public Transform OpenUISceneByName(string _name)
    {
        string path = "Prefabs/UI/" + _name;
        Object obj = Resources.Load(path);
        GameObject t = Object.Instantiate(obj) as GameObject;
        t.name = obj.name;
        if (null == t)
        {
            Debug.LogError("Fail to find obj from path:  " + path);
            return null;
        }

        t.transform.parent = g_UIAnchor.transform;
        t.transform.localScale = Vector3.one;
        t.transform.localPosition = Vector3.zero;
        t.transform.localRotation = Quaternion.identity;

        return t.transform;
    }

    static public void CloseUISceneByName(string name)
    {
        for (int i = 0; i < g_UIAnchor.transform.childCount; i++)
        {
            if (name == g_UIAnchor.transform.GetChild(i).name)
            {
                Object.Destroy(g_UIAnchor.transform.GetChild(i).gameObject);
                return;
            }
        }
    }

    static public Transform OpenUISceneByNameV1 (string _name)
    {
        if(!g_UIAnchor)
        {
            Debug.LogError("ui anchor is empty");
            return null;
        }

        for(int i = 0; i < g_UIAnchor.transform.childCount; i++)
        {
            if(_name == g_UIAnchor.transform.GetChild(i).name)
            {
                return g_UIAnchor.transform.GetChild(i);
            }
        }

        GameObject obj = Object.Instantiate(Resources.Load("Prefabs/UI/" + _name)) as GameObject;
        obj.transform.parent = g_UIAnchor.transform;
        return obj.transform;

        //判断当前anchor下面有没有名字是_name1的ui

        //ui路径是已知的, 根据路径加载预制体到层级列表

        //指明他的父亲游戏对象

        //返回刚才加载的ui transform

    }

    #endregion


}
