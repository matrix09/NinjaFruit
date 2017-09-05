using System.Collections.Generic;
using UnityEngine;
using AttTypeDefine;
public class GarbageCollection : MonoBehaviour {

    public static GarbageCollection m_Inst;

    Dictionary<eFruitType, List<GameObject>> m_dGCList;

    private void OnEnable()
    {
        m_Inst = this;
        m_dGCList = new Dictionary<eFruitType, List<GameObject>>();
    }

    public void OnDisable()
    {
        m_Inst = null;
        for (eFruitType type = eFruitType.Fruit_Melon; type <= eFruitType.Fruit_Size; type++)
        {
            if(m_dGCList.ContainsKey(type))
            {
                List<GameObject> list = m_dGCList[type];
                list.Clear();
                list = null;
            }
        }

        m_dGCList = null;

    }

    public GameObject GetGameObjectFromContainer (eFruitType type)
    {
        if(type <= eFruitType.Fruit_None || type >= eFruitType.Fruit_Size)
        {
            Debug.LogError("Input type is illeagl:)");
            return null;
        }
        if(m_dGCList.ContainsKey(type))
        {
            List<GameObject> list = m_dGCList[type];
            if (list.Count > 0)
            {
                GameObject obj = list[0];
                obj.SetActive(true);
                obj.transform.parent = null;
                list.RemoveAt(0);
                return obj;
            }
            else return null;
        }
        else 
        {
            return null;
        }
    }

    public void AddGameObjectToContainter (eFruitType type, GameObject obj)
    {
        if (
                (null == obj) || 
                (type <= eFruitType.Fruit_None || type >= eFruitType.Fruit_Size)
            )
        {
            Debug.LogError("Illegal input parameter type = " + type + " obj = " + obj);
            return;
        }

        obj.transform.parent = transform;
        obj.SetActive(false);
        List<GameObject> list;
        if (true == m_dGCList.ContainsKey(type))
        {
             list = m_dGCList[type];
        }
        else
        {
            list = new List<GameObject>();
            m_dGCList[type] = list;
        }
        list.Add(obj);
    }

}
