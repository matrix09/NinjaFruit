using Assets.Scripts.Utilites;
using AttTypeDefine;
using UnityEngine;

public class UIScene_Game : MonoBehaviour {

    #region 离开游戏
    public GameObject m_objLeftBtn;
    void LeaveGameScene(GameObject obj)
    {
        GlobalHelper.LoadLevel(eSceneName.Scene_Login);
    }
    #endregion

    #region 系统接口
    // Use this for initialization
    void Start () {
        UIEventListener.Get(m_objLeftBtn).onClick = LeaveGameScene;
        UIEventListener.Get(m_LeftGame).onClick = PressGameOverBtn;
        m_objGameOver.SetActive(false);
    }
    #endregion

    #region 注册event
    public void RegisterSliceMissle(Launcher _laun)
    {
        _laun.sliceMissle += new Launcher.SliceMissle(SliceMissle);
        _laun.sliceFruit += new Launcher.SliceMissle(SliceFruit);
    }
    public void RegisterDropBall (FruitController fc)
    {
        fc.m_delDropFruit = DropFruit;
    }

    #endregion

    #region 得分动画
    public UILabel m_labelScore;
    public TweenScale m_tsLabelScore;
    int m_nCurScore = 0;
    #endregion

    #region 游戏结束UI
    public GameObject m_objGameOver;
    public UILabel m_uiScore;
    public GameObject m_LeftGame;
    void PressGameOverBtn(GameObject obj)
    {
       
        GlobalHelper.LoadLevel(eSceneName.Scene_Login);
        GlobalHelper.ContinueGame();
    }
    #endregion

    #region 切中水果/雷的回调事件
    //切中雷
    void SliceMissle ()
    {
        m_objLeftBtn.SetActive(false);
        m_objGameOver.SetActive(true);
        m_uiScore.text = m_labelScore.text;
    }

    //切中水果
    void SliceFruit ()
    {
        m_nCurScore += 10;
        m_labelScore.text = m_nCurScore.ToString();

        m_tsLabelScore.ResetToBeginning();
        m_tsLabelScore.enabled = true;
    }
    #endregion

    #region 生命回调函数
    public GameObject m_objone;
    public GameObject m_objtwo;
    public GameObject m_objthree;
    int index = 0;
    void DropFruit ()
    {
        index++;
        switch (index)
        {
            case 1:
                {
                    m_objone.SetActive(true);
                    break;
                }
            case 2:
                {
                    m_objtwo.SetActive(true);
                    break;
                }
            case 3:
                {
                    m_objthree.SetActive(true);
                    break;
                }
            case 4:
                {
                    SliceMissle();
                    GameObject kc = GameObject.FindGameObjectWithTag("Knife");
                    Destroy(kc);
                    GlobalHelper.PauseGame();
                    break;
                }
        }
    }
    #endregion

}
