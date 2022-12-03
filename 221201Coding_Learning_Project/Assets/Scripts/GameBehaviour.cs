using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.SceneManagement;
using CustomExtensions;
using UnityEngine.Rendering.VirtualTexturing;

public class GameBehaviour : MonoBehaviour,IManager
{
    public delegate void DebugDelegate(string newText);

    public DebugDelegate debug = Print;
    public Stack<string> lootStack = new Stack<string>();
    private string _state;

    public string State
    {
        get { return _state;}
        set { _state = value; }
    }

    private void Start()
    {
        Initialize();
        InventoryList<string> inventoryList = new InventoryList<string>();
        inventoryList.SetItem("Potion");
        Debug.Log(inventoryList.item);
    }

    public void HandlePlayerJump()
    {
        debug("Player is jumped...");
    }

    public void Initialize()
    {
        _state = "Manager initailized...";
        _state.FancyDebug();
        Debug.Log(_state);
        debug(_state);
        LogWithDelegate(debug);
        
        GameObject player = GameObject.Find("Character");
        PlayerBehaviour playerBehaviour = player.GetComponent<PlayerBehaviour>();

        playerBehaviour.playerJump += HandlePlayerJump;
        
        
        lootStack.Push("HP+");
        lootStack.Push("Sword");
        lootStack.Push("Key");
        lootStack.Push("Boot");
        lootStack.Push("Bracers");
        
    }

    public static void Print(string newText)
    {
        Debug.Log(newText);
    }

    public void LogWithDelegate(DebugDelegate del)
    {
        del("Delegating the debug task...");
    }

    public bool showWinScreen = false;
    public bool showLossScreen = false;
    
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
                showWinScreen = true;
                PauseLevel("You win your freedom!");
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
            if (_playerHP > 0)
            {
                labelText = "Ouch...that's got hurt!";
            }
            else
            {
                showLossScreen = true;
                PauseLevel("You want another life with that?");
            }
        }
        
    }



    void PauseLevel(string label)
    {
        labelText = label;
        Time.timeScale = 0f;
    }

    private void OnGUI()
    {
        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You Win!"))
            {
                Utilities.RestartLevel();
            }
        }

        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You Lose!"))
            {
                try
                {
                    Utilities.RestartLevel(-1);
                    debug("Level restarted successfully...");

                }
                catch (System.ArgumentException e)
                {
                    Utilities.RestartLevel(0);
                    debug("Reverting to scene 0:" + e.ToString());
                }
                finally
                {
                    debug("Restart handled...");
                }
                
            }
        }

        GUI.Box(new Rect(20,20,150,25),"Player Health:"+_playerHP);
        GUI.Box(new Rect(20,50,150,25),"Items Collected:"+_itemsCollected);
        GUI.Box(new Rect(Screen.width/2 -150,Screen.height-50,300,40),labelText);
    }

    public void PrintLootReport()
    {
        var currentItem = lootStack.Pop();
        var nextItem = lootStack.Peek();
        Debug.LogFormat("You got a {0}! You've got a good chance of finding a {1} next!",currentItem,nextItem);
        Debug.LogFormat("There are {0} random loot items waiting for you!",lootStack.Count);
    }
}
