using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;

    public float surviveTime = 3;

    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ////基础设置
            //将输入的bullet对象克隆成一个暂时存在的实例
            GameObject bulletIns = Instantiate(bullet);
            //原本bullet的对象是隐藏的，实例化后转为true
            bulletIns.SetActive(true);
            //实例化后的bullet没有定义位置，确定使用当前挂载对象的位置
            //bullet脚本到底挂载在谁身上？其实这里调用的position也可以说明
            bulletIns.transform.position = transform.position;
            
            ////运动行为设置
            //代码中的脚本挂载.bulletMove不需要要挂载到bullet身上，直接用代码引用到这个component
            var moveCS = bulletIns.AddComponent<BulletMove>();
            //在bullet当中直接设置bulletMove的运动速度
            moveCS.bulletSpeed = speed;
            //实例化的对象存在一段时间后消失
            Destroy(bulletIns,surviveTime);
            
        }

    }
}
