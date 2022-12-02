using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{
    public bool showWinScreen = false;
    public string labelText = "Collect all Items and win your freedom!";
    public int maxItems = 5;
    
    private int _itemsCollected = 0;
    public int Items
    {
        get { return _itemsCollected;}
        set
        {
            _itemsCollected = value;
            if (_itemsCollected < maxItems)
            {
                labelText = (maxItems - _itemsCollected) + "Items you need to collect!";
            }

            if (_itemsCollected >= maxItems)
            {
                labelText = "You win your freedom!";
                showWinScreen = true;
                Time.timeScale = 0f;
            }
        }
    }
    
    private int _playerHP = 10;
    public int HP
    {
        get { return _playerHP;}
        set
        {
            _playerHP = value;
            Debug.LogFormat("Lives:{0}",_playerHP);
        }
    }

    private void OnGUI()
    {
        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You Win!"))
            {
                SceneManager.LoadScene(0);
                Time.timeScale = 1.0f;

            }

            
        }

        GUI.Box(new Rect(20,20,150,25),"Player Health:"+_playerHP);
        GUI.Box(new Rect(20,50,150,25),"Items Collected:"+_itemsCollected);
        GUI.Box(new Rect(Screen.width/2 -150,Screen.height-50,300,40),labelText);
    }
}
