using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // 设置敌人在玩家进入一定区域后就开始攻击的逻辑
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            Debug.Log("Attack!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Character")
        {
            Debug.Log("Leave me alone!");
        }
    }
}
