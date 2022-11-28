using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
//使用对象池时需要使用命名空间，并且仅适用于2021.1以上版本

public class FoodManager : MonoBehaviour
{
    public GameObject[] foods;

    public int number = 50;

    public bool useObjectPool;

    private ObjectPool<GameObject> foodpool;
    // Start is called before the first frame update
    void Start()
    {
        foodpool = new ObjectPool<GameObject>(() =>
            {
                var food = Instantiate(foods[Random.Range(0, foods.Length)], transform);
                food.AddComponent<Food>().destroyEvent.AddListener(() =>
                {
                    foodpool.Release(food);
                });
                return food;
            }, (go) =>
            {
                go.SetActive(true);
                go.transform.localPosition = Random.insideUnitSphere;
            }, (go) =>
            {
                // 失活
                go.SetActive(false);
            },
            (go) => { Destroy(go); });
    }

    // Update is called once per frame
    void Update()
    {
        if (useObjectPool)
        {
            for (int i = 0; i < number; i++)
            {
                foodpool.Get();
            }
        }
        else
        {
            for (int i = 0; i < number; i++)
            {
                var food = Instantiate(foods[Random.Range(0, foods.Length)],transform);
                // 返回半径为1的一个球体内的一个随机点
                // 随机球体范围
                food.transform.localPosition = Random.insideUnitCircle;
                // 使用代码来将之前已经写好的Food脚本挂载到目前实例化出来的food对象上，并且使用其中的destroyEvent事件
                food.AddComponent<Food>().destroyEvent.AddListener(() =>
                {
                    Destroy(food);
                });
            }
        }
        
    }
}
