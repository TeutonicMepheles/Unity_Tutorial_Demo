using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools 
{
    // 线段绘制的工具,算是笔刷！
    public static LineRenderer DrawLine(Transform tr, LineRenderer mline, Vector3 start, Vector3 end, Color color = default(Color), float width = 0.1f)
    {
        if (mline == null)
        {
            return DrawLine(tr, start, end, color, width);
        }

        mline.useWorldSpace = true;
        mline.SetVertexCount(2);
        mline.SetWidth(width,width);
        mline.material.SetColor("_Color",color);
        mline.startColor = color;
        mline.endColor = color;
        mline.SetPosition(0,start);
        mline.SetPosition(1,end);
        mline.useWorldSpace = false;
        return mline;
        
    }
    public static LineRenderer DrawLine(Transform tr, Vector3 start, Vector3 end, Color color = default(Color), float width = 0.1f)
    {
        GameObject go = new GameObject();
        go.name = "line";
        LineRenderer line = go.AddComponent<LineRenderer>();
        go.transform.SetParent(tr);
        line.useWorldSpace = true;
        line.material = ResLoad.Instance.LoadAsset<Material>("Material/LineMaterial");
        line.SetVertexCount(2);
        line.SetWidth(width,width);
        line.material.SetColor("_Color",color);
        line.startColor = color;
        line.endColor = color;
        line.useWorldSpace = false;
        return line;

    }
    
    // 把鼠标在屏幕当中的坐标转换为世界坐标
    // world to screen 世界坐标转换到屏幕坐标
    // screen to world 屏幕坐标转换到世界坐标
    public static Vector3 MouseWorldPosition(Vector3 target)
    {
        Vector3 targetScreenPoint = Camera.main.WorldToScreenPoint(target);
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetScreenPoint.z);
        Vector3 curWorldPoint = Camera.main.ScreenToWorldPoint(curScreenPoint);
        return curWorldPoint;
    }
}
