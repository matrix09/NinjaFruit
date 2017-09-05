using UnityEngine;
using AttTypeDefine;
using Assets.Scripts.Utilites;
public class Launcher : MonoBehaviour {

    private MeshRenderer mr;
    public MeshRenderer MR
    {
        get
        {
            if(null == mr)
            {
                mr = gameObject.GetComponent<MeshRenderer>();
            }
            return mr;
        }
    }

    float MaxSize
    {
        get
        {
            return MR.bounds.size.x > MR.bounds.size.y ? 2 * MR.bounds.size.x : 2 * MR.bounds.size.y;
        }
    }

    public delegate void SliceMissle();

    public event SliceMissle sliceMissle;

    public event SliceMissle sliceFruit;

    KnifeController m_kc;

    UIScene_Game m_uigame;
    public void OnStart ()
    {
        GlobalHelper.SetBackImg("background");

        //加载垃圾回收站
        GlobalHelper.LoadGarbageCollection ();

        //摆放launcher的位置
        float y = GlobalHelper.GetVisibleYPos(transform.position.z, MaxSize);
        Vector3 v = transform.position;
        transform.position = new Vector3(v.x, y, v.z);
        
        //生成水果
        InvokeRepeating("InstantiateFruites", 0.8f, 0.7f);

        //加载刀片
        m_kc = KnifeController.InstantiateMyKnife(v.z);

        //加载uiscene_game
        Transform ui = GlobalHelper.OpenUISceneByName("UIScene_Game");
        m_uigame = ui.GetComponent<UIScene_Game>();
        m_uigame.RegisterSliceMissle(this);
    }	

    void InstantiateFruites ()
    {
        eFruitType etype = eFruitType.Fruit_None;
        int n = Random.Range(1, 101);

        FruitController.SliceDelgate delSlice = FruitSliceEvent;

        if(n < 30)
        {
            etype = eFruitType.Fruit_Melon;
        }
        else if(n >= 30 && n < 60)
        {
            etype = eFruitType.Fruit_Lemon;
        }
        else if(n >= 60 && n < 90)
        {
            etype = eFruitType.Fruit_Pear;
        }
        else
        {
            delSlice = MissleSliceEvent;
            etype = eFruitType.Fruit_Missle;
        }

        FruitController fc =  FruitController.InstantiateMyFruit(
            transform.position, Vector3.zero, 
            eFruitState.FruitState_Game, 
            etype, delSlice);

        //注册坠球事件
        m_uigame.RegisterDropBall(fc);

        fc.RB.AddForce(
            new Vector3(
                Random.Range(-300, 300),
                Random.Range(600, 700),
                0
                )
            );

        fc.RB.AddTorque(
            new Vector3 (
                Random.Range(0, 360),
                Random.Range(0, 360),
                Random.Range(0, 360)
                )
            );
    }

    //切中水果事件
    void FruitSliceEvent (GameObject fruit, GameObject knife)
    {
        //播放切中特效
        sliceFruit();
    }

    //切中雷事件
    void MissleSliceEvent (GameObject fruit, GameObject knife)
    {
        Destroy(m_kc);
        GlobalHelper.PauseGame();
        sliceMissle();
    }
}
