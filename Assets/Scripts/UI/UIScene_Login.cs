using UnityEngine;
using AttTypeDefine;
public class UIScene_Login : MonoBehaviour {
    
    #region 开场图片
    public float m_fStartPicShownTime;//开场图片显示时间
    public UISprite m_uiShownPic;//开场显示的图片
    public float m_fStartPicAnimSpeed;//开场图片动画速度
    //开场图片显示完毕后的回调函数
    void DelayPlayShowPicAnim ()
    {
        m_eGameState = StartGameState.State_StartPicAnim;
    }
    #endregion
    
    #region 游戏名称动画
    public UIWidgetOffect m_UIOff;//游戏名称动画控制器
    public TweenPosition m_tp;//动画控件
    #endregion

    //登录UI状态
    StartGameState m_eGameState = StartGameState.State_None;

    //延迟播放游戏名称动画
    void DelayPlayGameNameAnim()
    {
        m_UIOff.TrigMove(UITweener.Method.BounceIn);

        //TweenPosition.Begin(m_UIOff.gameObject, duration, mIsTo ? mPos + toPos : mPos).method = m;

        Invoke("DelayDestroyGameNameAnim", m_UIOff.duration);
    }

    //动画结束后的回调函数
    void DelayDestroyGameNameAnim ()
    {
        Destroy(m_UIOff);
        Destroy(m_tp);
    }

    void Start()
    {
        //延时播放开场消失动画
        Invoke("DelayPlayShowPicAnim", m_fStartPicShownTime);
    }

    void Update () {
        //游戏开场动画图片
        if(m_eGameState == StartGameState.State_StartPicAnim/*游戏开场图片动画*/)
        {
            if(m_uiShownPic.fillAmount <= 0)
            {
                m_eGameState = StartGameState.State_StartGameNameAnim;/*游戏名称动画*/
            }
            else {
                m_uiShownPic.fillAmount -= (m_fStartPicAnimSpeed * Time.deltaTime);
            }
        }
        //游戏名称动画.
        else if(m_eGameState == StartGameState.State_StartGameNameAnim/*游戏名称动画*/)
        {
            DelayPlayGameNameAnim();
            m_eGameState = StartGameState.State_None;
        }
	}
}
