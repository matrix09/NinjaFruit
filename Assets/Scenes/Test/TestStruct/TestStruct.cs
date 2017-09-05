using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStruct : MonoBehaviour {

    struct MyStruct
    {
        public int m_nNum;
        //自我构造函数
        public MyStruct(int num)
        {
            m_nNum = num;
        }
        //系统具备默认的构造函数
    };


	void Start () {

        MyStruct mystruct;

        MyStruct struct2 = new MyStruct(12);

        mystruct.m_nNum = 11;
        struct2 = mystruct;
        mystruct.m_nNum = 13;
        Debug.Log(struct2.m_nNum);
        Debug.Log(mystruct.m_nNum);

    }

    private void Update()
    {
        //logic
    }

    private void FixedUpdate()
    {
        //physics
    }


}
