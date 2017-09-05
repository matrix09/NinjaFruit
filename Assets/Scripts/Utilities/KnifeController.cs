using UnityEngine;
namespace Assets.Scripts.Utilites
{
    public class KnifeController : MonoBehaviour
    {
        #region 自定义接口
        private bool m_bIsLoad = false;
        private float m_fZPos = 0f;
        //大片启动接口
        private void OnStart (float z)
        {
            m_fZPos = z;
            m_bIsLoad = true;
        }
        //设置刀片的可见性
        void SetKnifeVisible(bool _open)
        {
            m_SC.enabled = _open;
        }
        //实例化刀片
        public static KnifeController InstantiateMyKnife (float z)
        {
            GameObject obj = null;
            if( null == (obj = GameObject.FindGameObjectWithTag("Knife")))
            {
                obj = Instantiate(Resources.Load("Prefabs/Fruits/Knife")) as GameObject;
            }

            KnifeController kc = obj.GetOrAddComponent<KnifeController>();

            kc.OnStart(z);

            return kc;
        }
        #endregion

        #region 系统接口
        private SphereCollider m_SC;
        private void Awake()
        {
            m_SC = gameObject.GetComponent<SphereCollider>();
            SetKnifeVisible(false);
        }

        private void Update()
        {
            if(m_bIsLoad)
            {
                if(Input.GetMouseButtonDown (0))
                {
                    SetKnifeVisible(true);

                    transform.position = Camera.main.ScreenToWorldPoint(
                                        new Vector3(
                                            Input.mousePosition.x,
                                            Input.mousePosition.y,
                                            m_fZPos - Camera.main.transform.position.z                                        
                        ));

                }
                else if(Input.GetMouseButton(0))
                {
                    transform.position = Camera.main.ScreenToWorldPoint(
                                      new Vector3(
                                          Input.mousePosition.x,
                                          Input.mousePosition.y,
                                          m_fZPos - Camera.main.transform.position.z
                      ));
                }
                else if(Input.GetMouseButtonUp(0))
                {
                    SetKnifeVisible(false);
                }
            }
        }
        #endregion

    }

}
