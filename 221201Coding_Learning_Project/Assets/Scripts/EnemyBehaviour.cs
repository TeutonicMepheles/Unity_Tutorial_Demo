using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 使用AI敌人的额外命名空间
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform player;
    public Transform partolRoute;

    public List<Transform> location;
    private int locationIndex = 0;
    private NavMeshAgent agent;

    private int enemyLives = 5;

    public int EnemyLives
    {
        get { return enemyLives; }
        
        private set
        {
            enemyLives = value;

            if (enemyLives<=0)
            {
                Destroy(this.gameObject);
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Bullet(Clone)")
        {
            // 注意这里要使用的是get-set的方法来减敌人生命！
            EnemyLives -= 1;
            Debug.Log("Critical Hit!");
        }
        
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
        player = GameObject.Find("Character").transform;
    }

    private void Update()
    {
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    void MoveToNextPatrolLocation()
    {
        // 防御性编程，如果location为空，就使用return关键字推出执行
        if (location.Count == 0)
        {
            return;
        }
        
        // 实现敌人的巡逻和移动，不需要直接对transform进行操作，可以使用agent的有关方法
                // gameObject.transform.position
                // position/rotation/scale都是一个 vector3
                agent.destination = location[locationIndex].position;

                locationIndex = (locationIndex + 1) % location.Count;
    }

    void InitializePatrolRoute()
    {
        foreach (Transform child in partolRoute)
        {
            location.Add(child);
        }
    }

    // 设置敌人在玩家进入一定区域后就开始攻击的逻辑
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            agent.destination = player.position;
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
