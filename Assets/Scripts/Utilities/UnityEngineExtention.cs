using UnityEngine;

//静态方法扩展类
/*
 * 1 : 类需要是公有的且是静态的
 * 
 * 
 * */
public static class UnityEngineExtention  {
    
    public static T GetOrAddComponent<T> (this GameObject _mb) where T: Component
    {

        var relt = _mb.GetComponent<T>();

        if (null == relt)
        {
             relt = _mb.AddComponent<T>();
        }

        return relt;
    }
	
}
