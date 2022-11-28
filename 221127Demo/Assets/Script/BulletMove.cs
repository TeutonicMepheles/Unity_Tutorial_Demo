using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float bulletSpeed;
    // Update is called once per frame
    void Update()
    {
        //物体移动的基础控制
        //transform.position = Vector3.forward * Time.deltaTime * bulletSpeed;
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
    }
}
