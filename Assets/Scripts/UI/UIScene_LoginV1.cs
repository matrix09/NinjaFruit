using UnityEngine;
using AttTypeDefine;
using Assets.Scripts.Utilites;
public class UIScene_LoginV1 : MonoBehaviour {

    #region 开场图片动画
    //延迟动画
    public float m_fDelayTime;//延迟动画时间

    //延迟播放开场图片动画
    public void DelayPlayStartPic ()
    {
        m_eState = StartGameState.State_StartPicAnim;
    }

    //播放动画
    public UISprite m_UISprte;//ui的实例对象
    public float m_fPlaySpeed;//动画播放速度

    //登录状态
    StartGameState m_eState = StartGameState.State_None;
    #endregion

    #region 游戏名称动画
    public UIWidgetOffect m_uioffset;
    //延迟销毁uiwidgetoffset。
    void DelayDestroyWidgetOffSet ()
    {
        Destroy(m_uioffset);
        m_eState = StartGameState.State_FruitAnim;
    }
    #endregion

    #region 系统接口
    Vector3 m_vOrigPos;
    GameObject m_vFruit;
    public float m_fFruitMoveDuration;
    private void Awake()
    {
        //加载登录水果
        FruitController fc = FruitController.InstantiateMyFruit(
            Vector3.zero,//位置
            Vector3.zero,//旋转值
            eFruitState.FruitState_Login, eFruitType.Fruit_Melon, SliceEvent);

        //加载刀片   
        KnifeController.InstantiateMyKnife(fc.transform.position.z);

        //设置背景图片
        GlobalHelper.SetBackImg("LoginBackImg2");

    }
    // Use this for initialization
    void Start () {
        //延迟播放封面动画
        Invoke("DelayPlayStartPic", m_fDelayTime);
        //将水果放到屏幕外部
        //<获取水果>
        m_vFruit = GameObject.FindGameObjectWithTag("Fruit");
        FruitController fc = m_vFruit.GetComponent<FruitController>();
        float x = GlobalHelper.GetVisibleXPos(m_vFruit.transform.position.z, fc.MR.bounds.size.x > fc.MR.bounds.size.y ? fc.MR.bounds.size.x*2 : fc.MR.bounds.size.y*2);
        m_vOrigPos = m_vFruit.transform.position;
        m_vFruit.transform.position = new Vector3(x, m_vOrigPos.y, m_vOrigPos.z);
        //<获取水果>
    }	

	void Update () {
		
        if(m_eState == StartGameState.State_StartPicAnim)
        {
            if(m_UISprte.fillAmount <= 0)
            {
                //播放我的游戏名称动画
                m_eState = StartGameState.State_StartGameNameAnim;
            }
            else
            {
                //按照指定步长播放动画
                m_UISprte.fillAmount -= (m_fPlaySpeed * Time.deltaTime);
            }
        }
        else if(m_eState == StartGameState.State_StartGameNameAnim)
        {
            //播放动画
            m_uioffset.TrigMove(UITweener.Method.BounceIn);
            //延迟销毁
            Invoke("DelayDestroyWidgetOffSet", m_uioffset.duration);
            //更改当前界面状态
            m_eState = StartGameState.State_None;
        }
        else if(m_eState == StartGameState.State_FruitAnim)
        {
            m_eState = StartGameState.State_None;
            //播放水果动画
            TweenPosition.Begin(m_vFruit, m_fFruitMoveDuration, m_vOrigPos).method = UITweener.Method.BounceIn;
        }
	}
    #endregion

    #region 切水果代理事件
    void SliceEvent (GameObject knife, GameObject fruit)
    {
        //加载新场景
        GlobalHelper.LoadLevel(eSceneName.Scene_Game);
    }
    #endregion

}
