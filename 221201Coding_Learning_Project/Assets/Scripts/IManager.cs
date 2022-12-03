using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 使用interface关键字声明名为IManager的公共接口
public interface IManager
{
    string State { get; set; }

    void Initialize();
}

// 抽象类BaseManager 与 接口IManager具有相同的蓝图，允许任何子类使用override关键字定义他们的state和Initialize实现
public abstract class BaseManager
{
    protected string _state;
    public abstract string state { get; set; }
    public abstract void Initialize();
}

public class CombatManager : BaseManager
{
    public override string state
    {
        get { return _state; }
        set { _state = value; }
    }

    public override void Initialize()
    {
        _state = "Manager initialized...";
        Debug.Log(_state);
    }
}