using System;

/// <summary>
/// 不继承MONO的单例基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class Single<T>
    where T : class
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Type type = typeof(T);
                // 动态创建类的实例
                _instance = Activator.CreateInstance(type,true) as T;
            }
            return _instance;
        }
    }
    protected Single()
    { }
}