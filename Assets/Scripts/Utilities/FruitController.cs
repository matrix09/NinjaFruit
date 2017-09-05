using UnityEngine;
using AttTypeDefine;
namespace Assets.Scripts.Utilites
{
    public class FruitController : MonoBehaviour
    {

        #region 属性
        //get + set.
        private Rigidbody rb;
        //get.
        public Rigidbody RB // Property 属性
        {
            get
            {
                if (null == rb)
                {
                    rb = gameObject.GetOrAddComponent<Rigidbody>();
                }
                return rb;
            }
        }

        private MeshRenderer mr;
        public MeshRenderer MR
        {
            get
            {
                if (null == mr)
                    mr = gameObject.GetComponent<MeshRenderer>();
                return mr;
            }
        }

        public float MaxSize
        {
            get
            {
                return (MR.bounds.size.x > MR.bounds.size.y ? MR.bounds.size.x : MR.bounds.size.y) * 4;
            }
        }

        #endregion

        #region 变量
        //<切除水果代理事件>
        public delegate void SliceDelgate(GameObject knife, GameObject fruit);

        public delegate void DropFruit();

        public DropFruit m_delDropFruit;
        SliceDelgate m_delSliceEvent;
        //<切除水果代理事件>

        public eFruitType m_eFruitType = eFruitType.Fruit_None;
        public eFruitState m_eFruitState = eFruitState.FruitState_None;

        private GameObject m_oFrameObj = null;//外环游戏对象
        private float m_fYBtm = 0f;//水果下落深度
        #endregion

        #region 水果系统接口
        int nFt = 0;
        private void OnDisable()
        {
            m_eFruitType = eFruitType.Fruit_None;
            m_eFruitState = eFruitState.FruitState_None;
            m_oFrameObj = null;
            m_fYBtm = 0f;
            rb = null;
        }
        private void Update()
        {
            if (m_eFruitState == eFruitState.FruitState_Login)
            {
                //旋转 frame
                //旋转 水果
                if (null != m_oFrameObj)
                {
                    //旋转的速度设置
                    m_oFrameObj.transform.Rotate(2 * GlobalHelper.g_vRotateSpeed * Time.deltaTime);
                    transform.Rotate((Vector3.zero - GlobalHelper.g_vRotateSpeed) * Time.deltaTime);
                }
            }
            else if(m_eFruitState == eFruitState.FruitState_Game)
            {
                if(transform.position.y < m_fYBtm)
                {
                    nFt = (int)m_eFruitType;
                    if (nFt % 3 == 1)
                    {
                        m_delDropFruit();
                        //处理掉落事件，uiscene_game响应
                    }
                    GarbageCollection.m_Inst.AddGameObjectToContainter(m_eFruitType, gameObject);
                }
            }
        }
        #endregion

        #region 自定义接口
        //设置水果状态
        void SetFruitBehaviour (eFruitState state)
        {
            switch (state)
            {
                case eFruitState.FruitState_Login:
                    {
                        //设置刚体类型
                        RB.isKinematic = true;
                        //加载外环对象.
                        AddFruitFrame();
                        //路径
                        break;
                    }
                case eFruitState.FruitState_Game:
                    {
                        RB.isKinematic = false;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        //动态加载水果
        private void AddFruitFrame()
        {
            //动态加载一个预制体
            m_oFrameObj = Instantiate(Resources.Load("Prefabs/Fruits/BallFrame")) as GameObject;
            m_oFrameObj.transform.parent = transform;
            m_oFrameObj.transform.localPosition = Vector3.zero;
            m_oFrameObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
            m_oFrameObj.transform.localScale = new Vector3(2f, 2f, 2f);
        }

        //实例化水果
        public static FruitController InstantiateMyFruit(
                Vector3 pos,
                Vector3 rot,
                eFruitState state = eFruitState.FruitState_Game,
                eFruitType type = eFruitType.Fruit_Melon,
                SliceDelgate _sliceEvent = null
            )
        {

            GameObject fruit;
            //动态加载预制体到层级列表中
            if (null == GarbageCollection.m_Inst || null == (fruit = GarbageCollection.m_Inst.GetGameObjectFromContainer(type)))
            {
                fruit = Instantiate(Resources.Load(GlobalHelper.g_dGlobalInfos[type]), pos, Quaternion.Euler(rot)) as GameObject;
            }
            else
            {
                fruit.transform.position = pos;
                fruit.transform.rotation = Quaternion.Euler(rot);
            }

            FruitController fc = fruit.GetOrAddComponent<FruitController>();

            //设置水果状态对应的行为
            fc.SetFruitBehaviour(state);

            //设置水果类型
            fc.m_eFruitState = state;
            fc.m_eFruitType = type;


            //归零速度
            fc.RB.velocity = Vector3.zero;

            //设置离开视野坐标
            fc.m_fYBtm = GlobalHelper.GetVisibleYPos(pos.z, fc.MaxSize);

            if (null != _sliceEvent)
                fc.m_delSliceEvent = _sliceEvent;

            return fc;
        }
        #endregion
        Vector3 m_vOrigPos;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Knife")//如果是刀片
            {
                m_vOrigPos = other.gameObject.transform.position;
            }
        }
        #region 切水果事件
        Vector3 tempv;
        Quaternion tempq;
        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.tag == "Knife")//如果是刀片
            {
                #region 判断生成什么类型的水果
                eFruitType ft = m_eFruitType;

                int nFT = (int)ft;

                if (nFT % 3 == 1)
                {
                    ft += 1;
                }
                else
                    return;
                #endregion

                #region 切除水果事件
                m_delSliceEvent(other.gameObject, gameObject);
                #endregion

                #region 如果判断是雷，则直接返回，雷不需要生成两个小雷。
                if (m_eFruitType == eFruitType.Fruit_Missle)
                    return;
                #endregion
                AudioManager.PlayAudio(other.gameObject, eAudioType.Audio_CutFruit, "Melon");
                #region 播放切水果声音

                #endregion

                #region 播放轨迹特效
                tempv = (other.transform.position - m_vOrigPos);
                tempq = Quaternion.LookRotation(new Vector3(tempv.x, tempv.y, tempv.z));
                Instantiate(Resources.Load("Prefabs/Effects/Explosion 12"), transform.position, transform.rotation);
                Instantiate(Resources.Load("Prefabs/Effects/myflare13"), transform.position, tempq);
                #endregion

                #region 生成水果
                FruitController fc;
                Vector3 vPos;
                Vector3 vRot;
                //生成两个新的水果
                for (int i = 0; i < 2; i++)
                {
                    fc = InstantiateMyFruit(transform.position, transform.root.eulerAngles, eFruitState.FruitState_Game, ft);
                    if(i == 0)
                    {
                        vPos = new Vector3(
                            Random.Range(-80f, -40f),
                            Random.Range(20f, 40f),
                            0);

                        vRot = new Vector3(
                            Random.Range(-300, -200f),
                            Random.Range(100f, 200f),
                            Random.Range(-200f, -100f));

                    }
                    else
                    {
                        vPos = new Vector3(
                           Random.Range(40f, 80f),
                           Random.Range(20f, 40f),
                           0);

                        vRot = new Vector3(
                          Random.Range(200, 300f),
                          Random.Range(100f, 200f),
                          Random.Range(100f, 200f));

                    }

                    fc.RB.AddForce(vPos);
                    fc.RB.AddTorque(vRot);
                }
                #endregion

                #region 将原来的水果放到回收站中
                if (m_eFruitState == eFruitState.FruitState_Game)
                    GarbageCollection.m_Inst.AddGameObjectToContainter(m_eFruitType, gameObject);
                else if (m_eFruitState == eFruitState.FruitState_Login)
                    Destroy(gameObject);
                #endregion
            }
        }
        #endregion

    }
}

