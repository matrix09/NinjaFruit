using UnityEngine;
using AttTypeDefine;
using Assets.Scripts.Utilites;
public class StartUp : MonoBehaviour {

    public bool m_bIsOpenLog;
    

    void Awake()
    {
        //日志控制器
        GlobalHelper.g_bIsOpenLog = m_bIsOpenLog;

        //播放背景音乐
        AudioManager.PlayAudio(null, eAudioType.Audio_BackGround, "Login");

        //初始化全局数据
        GlobalHelper.InitGlobalDatas();

        //加载UI数据
       GlobalHelper.LoadUIRoot();
        
        //打开登录场景
       GlobalHelper.OpenUISceneByName("UIScene_LoginV1");
       
    }

    void Start () {

        Destroy(gameObject);

	}

}
