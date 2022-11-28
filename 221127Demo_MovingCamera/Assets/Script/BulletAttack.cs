using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("开始触发");
        var enemyCS = other.gameObject.GetComponent<Enemy>();
        if (enemyCS)
        {
            if (enemyCS.hp > 0)
            {
                enemyCS.hp--;
            }

            if (enemyCS.hp==0)
            {
                Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
