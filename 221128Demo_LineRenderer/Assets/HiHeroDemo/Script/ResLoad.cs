using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// Resources资源加载
/// </summary>
public class ResLoad : SingleMono<ResLoad>
{
    public GameObject LoadPrefab(string resName)
    {
        GameObject go = Resources.Load<GameObject>(resName);
        if (go == null)
        {
            Debug.Log(resName);
            Debug.Log("ResLoad: Resources加载路径加载失败");
            return null;
        }
        return Instantiate(go);
    }
    
    public GameObject LoadPrefab(GameObject go)
    {
        if (go == null)
        {
            Debug.Log("ResLoad: GameObject加载失败");
            return null;
        }
        return Instantiate(go);
    }
		
    public T LoadAsset<T>(string pathName, string resName)
        where T : Object
    {
        return Resources.Load<T>(pathName + "/" + resName);
    }
    
    public T LoadAsset<T>(string resName)
        where T : Object
    {
        return Resources.Load<T>(resName);
    }
}