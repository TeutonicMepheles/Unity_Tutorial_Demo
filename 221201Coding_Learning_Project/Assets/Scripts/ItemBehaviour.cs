using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public GameBehaviour gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameBehaviour>();
    }

    // 写一个为药丸设定的碰撞后被拾取的逻辑
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Character")
        {
            Destroy(transform.parent.gameObject);
            Debug.Log("Item Collected!");
        }
        gameManager.Items += 1;
        gameManager.PrintLootReport();
    }
}
