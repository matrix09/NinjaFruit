using System.Collections;
using UnityEngine;

public class UIScene_Progress : MonoBehaviour {

    #region 定义数据类型
    enum eProgressState
    {
        State_None,
        State_BlackOut,
        State_ProgressBar,
        State_BlackIn,
    }
    //<进度状态>
    eProgressState m_eState = eProgressState.State_None;
    #endregion

    #region 黑幕
    //<黑幕动画速度>
    public float m_fBlackSpeed;
    //<黑幕sprite>
    public UISprite m_uiBlack;
    #endregion

    #region 进度条
    //<进度条速度>
    float m_fProgressSpeed;
    //<进度条slide>
    public UISlider m_sliProgressBar;
    //<进度条label>
    public UILabel m_uiLabel;

    //设置进度条UI
    void SetProgressBarPercent()
    {
        m_sliProgressBar.value += (m_fProgressSpeed);
        int num = (int)(m_sliProgressBar.value * 100);
        m_uiLabel.text = num.ToString() + "%";
    }

    #endregion

    #region 系统接口
    private void Awake()
    {
        //激活黑幕
        m_uiBlack.gameObject.SetActive(true);
    }
    // Use this for initialization
    void Start () {
        StartCoroutine(LoadNewScene());
    }
	
	// Update is called once per frame
	void Update () {
		
        if(null != m_ao && m_ao.isDone && !m_bIsFirstTime)
        {
            m_eState = eProgressState.State_BlackOut;
            GlobalHelper.LoadSceneRes();
            m_bIsFirstTime = true;
        }

        if (!m_bIsFirstTime)
            return;

        //<黑幕动画>
        if(m_eState == eProgressState.State_BlackOut)
        {
            if(m_uiBlack.fillAmount <= 0)
            {
                m_eState = eProgressState.State_ProgressBar;
            }
            else
            {
                m_uiBlack.fillAmount -= (m_fBlackSpeed / 100f) * Time.deltaTime;
            }
        }
        //<进度条动画>
        else if(m_eState == eProgressState.State_ProgressBar)
        {
            m_fProgressSpeed = Random.Range(0.05f, 0.09f);

            if (m_sliProgressBar.value >= 1.0f)
            {
                m_eState = eProgressState.State_BlackIn;
            }
        }
        //<离开loading场景动画>
        else if(m_eState == eProgressState.State_BlackIn)
        {
            if (m_uiBlack.fillAmount >= 1.0f)
            {
                m_eState = eProgressState.State_None;
                DestroySelf();
            }
            else
            {
                m_uiBlack.fillAmount += (m_fBlackSpeed / 100f) * Time.deltaTime;
            }
        }

        if (m_sliProgressBar.value < 1.0f)
            SetProgressBarPercent();
    }
    #endregion

    #region 销毁自己
    public GameObject m_objRoot;
    void DestroySelf ()
    {
        //GlobalHelper.CloseUISceneByName(name);
        Destroy(m_objRoot);
    }
    #endregion

    #region 异步加载场景
    bool m_bIsFirstTime = false;
    AsyncOperation m_ao;
    IEnumerator LoadNewScene ()
    {
        m_ao = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(
            GlobalHelper.g_CurSceneInfo.m_strSceneName);
        yield return m_ao;
        int a = 0;


    }
    #endregion

}
